Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInspectionStndProcess
    Inherits System.Windows.Forms.Form
    Dim RsInspectionMain As ADODB.Recordset
    Dim RsInspectionDetail As ADODB.Recordset
    Dim RsInspectionRev As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Dim xMenuID As String

    Private Const ColOPR As Short = 1
    Private Const ColDetailType As Short = 2
    Private Const ColParamDesc As Short = 3
    Private Const ColStdClass As Short = 4
    Private Const ColSpecification As Short = 5
    Private Const ColInspectionMth As Short = 6
    Private Const ColDataFrom As Short = 7
    Private Const ColControlMth As Short = 8

    Private Const ColRevNo As Short = 1
    Private Const ColModDet As Short = 2
    Private Const ColModByCode As Short = 3
    Private Const ColModByName As Short = 4
    Private Const ColDate As Short = 5


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtStandardNo.Enabled = False
            cmdSearchStndNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsInspectionMain.EOF = False Then RsInspectionMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtStandardNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsInspectionMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_INSPECTION_STD_HDR", (txtStandardNo.Text), RsInspectionMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "QAL_INSPECTION_STD_HDR", "AUTO_KEY_STD", (txtStandardNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_INSPECTION_STD_DETR WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_INSPECTION_STD_DET WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_INSPECTION_STD_HDR WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsInspectionMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsInspectionMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsInspectionMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtStandardNo.Enabled = False
            cmdSearchStndNo.Enabled = False
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
            txtStandardNo_Validating(txtStandardNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function CheckDuplicateSpecfic(ByRef pSpecification As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If pSpecification = "" Then CheckDuplicateSpecfic = False : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColSpecification
                If UCase(Trim(.Text)) = UCase(Trim(pSpecification)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateSpecfic = True
                        MsgInformation("Duplicate Specification")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColSpecification)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double
        Dim mStage As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If cboStage.Text = "Receipt Inspection" Then
            mStage = "R"
        ElseIf cboStage.Text = "Final Inspection" Then
            mStage = "F"
        ElseIf cboStage.Text = "Layout Inspection" Then
            mStage = "L"
        ElseIf cboStage.Text = "Doc Audit Inspection" Then
            mStage = "D"
        ElseIf cboStage.Text = "Preventive Maintenance" Then
            mStage = "M"
        ElseIf cboStage.Text = "Predictive Maintenance" Then
            mStage = "C"
        ElseIf cboStage.Text = "Electro Plating Inspection" Then
            mStage = "E"
        ElseIf cboStage.Text = "Painted / Powder Coated Inspection" Then
            mStage = "A"
        ElseIf cboStage.Text = "Gauge / Fixture Inspection" Then
            mStage = "G"
        ElseIf cboStage.Text = "Initial Sample Parts" Then
            mStage = "I"
        ElseIf cboStage.Text = "Process Inspection" Then
            mStage = "P"
        End If

        SqlStr = ""
        mSlipNo = Val(txtStandardNo.Text)
        If Val(txtStandardNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtStandardNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_INSPECTION_STD_HDR " & vbCrLf _
                            & " (AUTO_KEY_STD,COMPANY_CODE," & vbCrLf _
                            & " INSP_TYPE,ITEM_CODE,DOC_NO,DOC_DATE,REV_NO,REV_DATE,IDEN, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " '" & mStage & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDocNo.Text) & "',TO_DATE('" & vb6.Format(txtDocDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRevNo.Text) & "',TO_DATE('" & vb6.Format(txtRevDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtIDEN.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_INSPECTION_STD_HDR SET " & vbCrLf _
                    & " AUTO_KEY_STD=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                    & " INSP_TYPE='" & mStage & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                    & " DOC_NO='" & MainClass.AllowSingleQuote(txtDocNo.Text) & "',DOC_DATE=TO_DATE('" & vb6.Format(txtDocDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " REV_NO='" & MainClass.AllowSingleQuote(txtRevNo.Text) & "',REV_DATE=TO_DATE('" & vb6.Format(txtRevDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " IDEN='" & MainClass.AllowSingleQuote(txtIDEN.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_STD =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        If UpdateRev = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtStandardNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsInspectionMain.Requery()
        RsInspectionDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_STD)  " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_STD,LENGTH(AUTO_KEY_STD)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mDetailType As String
        Dim mParamDesc As String
        Dim mStdClass As String
        Dim mSpecification As String
        Dim mInspectionMth As String
        Dim mDataFrom As String
        Dim mControlMth As String
        Dim pOPRDesc As String
        Dim pOPRCode As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset

        PubDBCn.Execute("DELETE FROM QAL_INSPECTION_STD_DET WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDetailType
                mDetailType = Trim(.Text)

                .Col = ColParamDesc
                mParamDesc = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColStdClass
                mStdClass = VB.Left(Trim(.Text), 2)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColInspectionMth
                mInspectionMth = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColDataFrom
                mDataFrom = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColControlMth
                mControlMth = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColOPR
                pOPRDesc = Trim(.Text)
                If Trim(pOPRDesc) = "" Then
                    pOPRCode = ""
                Else
                    pSqlStr = " SELECT IMST.OPR_CODE " & vbCrLf & " FROM PRD_OPR_MST IMST, PRD_OPR_TRN TRN" & vbCrLf & " WHERE IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IMST.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IMST.OPR_CODE=TRN.OPR_CODE" & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'" & vbCrLf & " AND IMST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    Else
                        pOPRCode = ""
                    End If
                End If

                SqlStr = ""

                If mDetailType <> "" And mParamDesc <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_INSPECTION_STD_DET ( " & vbCrLf & " AUTO_KEY_STD,SERIAL_NO,DETAIL_TYPE,PARAM_DESC, " & vbCrLf & " STD_CLASS,SPECIFICATION,INSP_MTH,DATA_FORM,CONTROL_METHOD, OPR_CODE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mDetailType & "', " & vbCrLf & " '" & mParamDesc & "','" & mStdClass & "', " & vbCrLf & " '" & mSpecification & "','" & mInspectionMth & "','" & mDataFrom & "', " & vbCrLf & " '" & mControlMth & "','" & pOPRCode & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateRev() As Boolean

        On Error GoTo UpdateRevERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRevNo As String
        Dim mModDet As String
        Dim mModByCode As String
        Dim mDate As String


        PubDBCn.Execute("DELETE FROM QAL_INSPECTION_STD_DETR WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "")

        With SprdRev
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColRevNo
                mRevNo = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColModDet
                mModDet = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColModByCode
                mModByCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColDate
                mDate = VB6.Format(Trim(.Text), "DD/MMM/YYYY")


                SqlStr = ""

                If Trim(mRevNo) <> "" Then
                    SqlStr = " INSERT INTO  QAL_INSPECTION_STD_DETR ( " & vbCrLf & " AUTO_KEY_STD,SERIAL_NO,REV_NO,MOD_DET, " & vbCrLf & " MOD_BY,MOD_DATE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mRevNo & "', " & vbCrLf & " '" & mModDet & "','" & mModByCode & "', " & vbCrLf & " '" & mDate & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateRev = True
        Exit Function
UpdateRevERR:
        UpdateRev = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub cmdSearchICode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchICode.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            lblItemCode.text = AcName
            txtItemCode.Text = AcName1
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
    End Sub
    Private Sub cmdSearchStndNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchStndNo.Click
        Dim SqlStr As String
        SqlStr = " SELECT QAL_INSPECTION_STD_HDR.AUTO_KEY_STD, QAL_INSPECTION_STD_HDR.INSP_TYPE, " & vbCrLf & " QAL_INSPECTION_STD_HDR.ITEM_CODE, INV_ITEM_MST.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR, INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.COMPANY_CODE = INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE = INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtStandardNo.Text = AcName
            Call txtStandardNo_Validating(txtStandardNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsInspectionMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmInspectionStndProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Inspection Standard (Process)"

        SqlStr = "Select * From QAL_INSPECTION_STD_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_INSPECTION_STD_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_INSPECTION_STD_DETR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionRev, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_STD AS STANDARD_NUMBER,INSP_TYPE,DOC_NO, " & vbCrLf & " TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE,ITEM_CODE " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INSP_TYPE='P'" & vbCrLf & " ORDER BY ITEM_CODE,AUTO_KEY_STD"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmInspectionStndProcess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmInspectionStndProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        xMenuID = myMenu
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7065)
        'Me.Width = VB6.TwipsToPixelsX(11370)

        Call FillCombo()

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FillCombo()
        cboStage.Items.Clear()
        '    cboStage.AddItem "Receipt Inspection"
        '    cboStage.AddItem "Final Inspection"
        '    cboStage.AddItem "Layout Inspection"
        '    cboStage.AddItem "Doc Audit Inspection"
        '    cboStage.AddItem "Preventive Maintenance"
        '    cboStage.AddItem "Predictive Maintenance"
        '    cboStage.AddItem "Electro Plating Inspection"
        '    cboStage.AddItem "Painted / Powder Coated Inspection"
        '    cboStage.AddItem "Gauge / Fixture Inspection"
        '    cboStage.AddItem "Initial Sample Parts"
        cboStage.Items.Add("Process Inspection")
        cboStage.SelectedIndex = 0

    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtStandardNo.Text = ""
        cboStage.SelectedIndex = 0
        txtItemCode.Text = ""
        lblItemCode.Text = ""
        txtPartNo.Text = ""
        txtDocNo.Text = ""
        txtDocDate.Text = ""
        txtRevNo.Text = ""
        txtRevDate.Text = ""
        txtIDEN.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ClearGrid(SprdRev, ConRowHeight)
        FormatSprdRev(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsInspectionMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColDetailType
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "A" & Chr(9) & "B" & Chr(9) & "C" & Chr(9) & "D" & Chr(9) & "E"
            .TypeComboBoxCurSel = 0

            .Col = ColParamDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspectionDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColStdClass
            .CellType = SS_CELL_TYPE_COMBOBOX
            .TypeComboBoxList = "Major" & Chr(9) & "Minor" & Chr(9) & "Critical" & Chr(9) & "CQ" & Chr(9) & " "
            .TypeComboBoxCurSel = 4

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspectionDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColInspectionMth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspectionDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColDataFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspectionDetail.Fields("DATA_FORM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColControlMth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspectionDetail.Fields("CONTROL_METHOD").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColOPR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 14) '' 7.5

            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub FormatSprdRev(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String


        With SprdRev
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColRevNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsInspectionRev.Fields("REV_NO").DefinedSize

            .Col = ColModDet
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsInspectionRev.Fields("MOD_DET").DefinedSize

            .Col = ColModByCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsInspectionRev.Fields("MOD_BY").DefinedSize

            .Col = ColModByName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY

            MainClass.ProtectCell(SprdRev, 1, .MaxRows, ColModByName, ColModByName)
            MainClass.SetSpreadColor(SprdRev, Arow)
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
            .set_ColWidth(2, 500 * 5)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtStandardNo.Maxlength = RsInspectionMain.Fields("AUTO_KEY_STD").Precision
        txtItemCode.Maxlength = RsInspectionMain.Fields("ITEM_CODE").DefinedSize
        txtDocNo.Maxlength = RsInspectionMain.Fields("DOC_NO").DefinedSize
        txtDocDate.Maxlength = RsInspectionMain.Fields("DOC_DATE").DefinedSize - 6
        txtRevNo.Maxlength = RsInspectionMain.Fields("REV_NO").DefinedSize
        txtRevDate.Maxlength = RsInspectionMain.Fields("REV_DATE").DefinedSize - 6
        txtIDEN.Maxlength = RsInspectionMain.Fields("IDEN").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mOprDesc As String
        Dim pOPRCode As String
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim cntRow As Integer

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsInspectionMain.EOF = True Then Exit Function

        If Trim(cboStage.Text) = "" Then
            MsgInformation("Stage is empty, So unable to save.")
            cboStage.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty, So unable to save.")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColOPR
                mOprDesc = Trim(.Text)
                pOPRCode = ""
                If mOprDesc <> "" Then
                    '        SqlStr = OperationQuery(Trim(txtItemCode.Text), "", "", "", Format(PubCurrDate, "DD/MM/YYYY"), "TRN.OPR_CODE")
                    '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
                    '
                    '        If RsTemp.EOF = False Then
                    '            MsgInformation "Operation Defined for Item Code : " & mProductCode & ". Cann't Be Saved"
                    '            FieldsVarification = False
                    '            MainClass.SetFocusToCell SprdMain, cntRow, ColOPR
                    '            Exit Function
                    '        End If
                    '    Else
                    SqlStr = OperationQuery(Trim(txtItemCode.Text), "", "", Trim(mOprDesc), VB6.Format(PubCurrDate, "DD/MM/YYYY"), "TRN.OPR_CODE")
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = True Then
                        MsgInformation("Invalid Operation for Item Code : " & txtItemCode.Text & ". Cann't Be Saved")
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColOPR)
                        Exit Function
                    Else
                        pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    End If
                End If
            Next
        End With

        If MainClass.ValidDataInGrid(SprdMain, ColDetailType, "S", "Please Check Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColParamDesc, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColInspectionMth, "S", "Please Check Inspection Method.") = False Then FieldsVarification = False
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmInspectionStndProcess_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsInspectionMain.Close()
        RsInspectionMain = Nothing
        RsInspectionDetail.Close()
        RsInspectionDetail = Nothing
        RsInspectionRev.Close()
        RsInspectionRev = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub



    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        Dim mProductCode As String

        mProductCode = Trim(txtItemCode.Text)

        If eventArgs.Row = 0 And eventArgs.Col = ColOPR Then
            With SprdMain
                .Row = .ActiveRow

                If Trim(mProductCode) <> "" Then
                    .Col = ColOPR

                    SqlStr = OperationQuery(Trim(mProductCode), "", "", Trim(.Text), VB6.Format(PubCurrDate, "DD/MM/YYYY"), "TRIM(TO_CHAR(OPR_SNO,'00')) || '-' || MST.OPR_DESC", "TRN.OPR_CODE", "TO_CHAR(OPR_SNO)")

                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow

                        .Col = ColOPR
                        .Text = Trim(Mid(AcName, 4))
                    End If
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPR, .ActiveRow, ColOPR, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDetailType)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xDetailType As String
        Dim xParamDesc As String
        Dim xSpecification As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColDetailType
        xDetailType = Trim(SprdMain.Text)
        If xDetailType = "" Then Exit Sub

        SprdMain.Col = ColParamDesc
        xParamDesc = Trim(SprdMain.Text)
        If xParamDesc = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColOPR
                '            If DuplicateItem = True Then
                '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColOPR ' SprdMain.ActiveRow
                '                Cancel = True
                '                Exit Sub
                '            End If
                Call CheckOPR()
            Case ColParamDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColParamDesc
                xParamDesc = Trim(SprdMain.Text)
                If xParamDesc = "" Then Exit Sub

            Case ColSpecification
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColSpecification
                xSpecification = Trim(SprdMain.Text)
                If xSpecification = "" Then Exit Sub
                '            If CheckDuplicateSpecfic(xSpecification) = False Then
                MainClass.AddBlankSprdRow(SprdMain, ColSpecification, ConRowHeight)
                FormatSprdMain((SprdMain.MaxRows))
                '            End If

        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CheckOPR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mProductCode As String

        mProductCode = Trim(txtItemCode.Text)

        With SprdMain
            .Row = .ActiveRow

            .Col = ColOPR
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = OperationQuery(Trim(mProductCode), "", "", Trim(.Text), VB6.Format(PubCurrDate, "DD/MM/YYYY"), "TRN.OPR_CODE")

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operation for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOPR)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdRev_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdRev.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdRev_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdRev.ClickEvent

        Dim SqlStr As String
        If eventArgs.Row = 0 And eventArgs.Col = ColModByCode Then
            With SprdRev
                .Row = .ActiveRow
                .Col = ColModByCode
                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColModByCode
                    .Text = Trim(AcName)

                    .Col = ColModByName
                    .Text = Trim(AcName1)
                End If
                Call SprdRev_LeaveCell(SprdRev, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColModByCode, .ActiveRow, ColModByCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColModByName Then
            With SprdRev
                .Row = .ActiveRow
                .Col = ColModByName

                If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColModByCode
                    .Text = Trim(AcName1)

                    .Col = ColModByName
                    .Text = Trim(AcName)
                End If
                Call SprdRev_LeaveCell(SprdRev, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColModByCode, .ActiveRow, ColModByCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdRev, eventArgs.Row, ColRevNo)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdRev_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdRev.KeyUpEvent
        Dim mCol As Short
        mCol = SprdRev.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColModByCode Then SprdRev_ClickEvent(SprdRev, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModByCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColModByName Then SprdRev_ClickEvent(SprdRev, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModByName, 0))
        SprdRev.Refresh()
    End Sub

    Private Sub SprdRev_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdRev.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdRev.Row = SprdRev.ActiveRow

        SprdRev.Col = ColRevNo
        If Trim(SprdRev.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColRevNo
                SprdRev.Row = SprdRev.ActiveRow

                SprdRev.Col = ColRevNo
                If Trim(SprdRev.Text) = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdRev, ColRevNo, ConRowHeight)
                FormatSprdRev((SprdRev.MaxRows))
            Case ColModByCode
                Call CheckEmp(ColModByCode, (SprdRev.ActiveRow))
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CheckEmp(ByRef pCol As Integer, ByRef pRow As Integer)

        On Error GoTo EmpERR
        With SprdRev
            .Row = .ActiveRow
            .Col = ColModByCode
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(SprdRev.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                .Row = .ActiveRow
                .Col = ColModByName
                .Text = ""
                MainClass.SetFocusToCell(SprdRev, SprdRev.ActiveRow, ColModByCode)
            Else
                .Row = .ActiveRow
                .Col = ColModByName
                .Text = MasterNo
            End If
        End With
        Exit Sub
EmpERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdRev_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdRev.Leave
        With SprdRev
            SprdRev_LeaveCell(SprdRev, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtStandardNo.Text = SprdView.Text
        txtStandardNo_Validating(txtStandardNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtDocDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDocDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDocDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDocDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDocNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDocNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIDEN_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIDEN.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIDEN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIDEN.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIDEN.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call cmdSearchICode_Click(cmdSearchICode, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchICode_Click(cmdSearchICode, New System.EventArgs())
    End Sub
    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ValMacERR
        Dim SqlStr As String
        Dim mStage As String

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Item Code Does Not Exist In Master.")
            Cancel = True
        Else
            lblItemCode.Text = MasterNo
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtPartNo.Text = MasterNo
            End If

            If IsShowing = True Then GoTo EventExitSub
            If cboStage.Text = "Receipt Inspection" Then
                mStage = "R"
            ElseIf cboStage.Text = "Final Inspection" Then
                mStage = "F"
            ElseIf cboStage.Text = "Layout Inspection" Then
                mStage = "L"
            ElseIf cboStage.Text = "Doc Audit Inspection" Then
                mStage = "D"
            ElseIf cboStage.Text = "Preventive Maintenance" Then
                mStage = "M"
            ElseIf cboStage.Text = "Predictive Maintenance" Then
                mStage = "C"
            ElseIf cboStage.Text = "Electro Plating Inspection" Then
                mStage = "E"
            ElseIf cboStage.Text = "Painted / Powder Coated Inspection" Then
                mStage = "A"
            ElseIf cboStage.Text = "Gauge / Fixture Inspection" Then
                mStage = "G"
            ElseIf cboStage.Text = "Initial Sample Parts" Then
                mStage = "I"
            ElseIf cboStage.Text = "Process Inspection" Then
                mStage = "P"
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INSP_TYPE='" & mStage & "' "
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "AUTO_KEY_STD", "QAL_INSPECTION_STD_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
                txtStandardNo.Text = MasterNo
                txtStandardNo_Validating(txtStandardNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
            End If
        End If
        GoTo EventExitSub
ValMacERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRevDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRevDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRevDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRevDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRevDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRevDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRevNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRevNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStandardNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStandardNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsInspectionMain.EOF Then
            IsShowing = True

            lblMkey.Text = IIf(IsDbNull(RsInspectionMain.Fields("AUTO_KEY_STD").Value), "", RsInspectionMain.Fields("AUTO_KEY_STD").Value)
            txtStandardNo.Text = IIf(IsDbNull(RsInspectionMain.Fields("AUTO_KEY_STD").Value), "", RsInspectionMain.Fields("AUTO_KEY_STD").Value)

            If IsDbNull(RsInspectionMain.Fields("INSP_TYPE").Value) Then
                cboStage.SelectedIndex = -1
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "R" Then
                cboStage.Text = "Receipt Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "F" Then
                cboStage.Text = "Final Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "L" Then
                cboStage.Text = "Layout Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "D" Then
                cboStage.Text = "Doc Audit Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "M" Then
                cboStage.Text = "Preventive Maintenance"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "C" Then
                cboStage.Text = "Predictive Maintenance"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "E" Then
                cboStage.Text = "Electro Plating Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "A" Then
                cboStage.Text = "Painted / Powder Coated Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "G" Then
                cboStage.Text = "Gauge / Fixture Inspection"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "I" Then
                cboStage.Text = "Initial Sample Parts"
            ElseIf RsInspectionMain.Fields("INSP_TYPE").Value = "P" Then
                cboStage.Text = "Process Inspection"
            End If

            txtItemCode.Text = IIf(IsDbNull(RsInspectionMain.Fields("ITEM_CODE").Value), "", RsInspectionMain.Fields("ITEM_CODE").Value)
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
            txtDocNo.Text = IIf(IsDbNull(RsInspectionMain.Fields("DOC_NO").Value), "", RsInspectionMain.Fields("DOC_NO").Value)
            txtDocDate.Text = IIf(IsDbNull(RsInspectionMain.Fields("DOC_DATE").Value), "", RsInspectionMain.Fields("DOC_DATE").Value)
            txtRevNo.Text = IIf(IsDbNull(RsInspectionMain.Fields("REV_NO").Value), "", RsInspectionMain.Fields("REV_NO").Value)
            txtRevDate.Text = IIf(IsDbNull(RsInspectionMain.Fields("REV_DATE").Value), "", RsInspectionMain.Fields("REV_DATE").Value)
            txtIDEN.Text = IIf(IsDbNull(RsInspectionMain.Fields("IDEN").Value), "", RsInspectionMain.Fields("IDEN").Value)
            Call ShowDetail1()
            Call ShowRev()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtStandardNo.Enabled = True
        cmdSearchStndNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsInspectionMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mOprCode As String
        Dim mOprDesc As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_INSPECTION_STD_DET " & vbCrLf & " WHERE AUTO_KEY_STD=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsInspectionDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColDetailType
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DETAIL_TYPE").Value), "", .Fields("DETAIL_TYPE").Value))

                SprdMain.Col = ColParamDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColStdClass
                If IsDbNull(.Fields("STD_CLASS").Value) Then
                    SprdMain.Text = ""
                ElseIf Trim(.Fields("STD_CLASS").Value) = "Ma" Then
                    SprdMain.Text = "Major"
                ElseIf Trim(.Fields("STD_CLASS").Value) = "Mi" Then
                    SprdMain.Text = "Minor"
                ElseIf Trim(.Fields("STD_CLASS").Value) = "Cr" Then
                    SprdMain.Text = "Critical"
                ElseIf UCase(Trim(.Fields("STD_CLASS").Value)) = "CQ" Then
                    SprdMain.Text = "CQ"
                End If

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspectionMth
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColDataFrom
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DATA_FORM").Value), "", .Fields("DATA_FORM").Value))

                SprdMain.Col = ColControlMth
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CONTROL_METHOD").Value), "", .Fields("CONTROL_METHOD").Value))


                mOprCode = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                If MainClass.ValidateWithMasterTable(mOprCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOprDesc = MasterNo
                Else
                    mOprDesc = ""
                End If
                SprdMain.Col = ColOPR
                SprdMain.Text = mOprDesc


                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowRev()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT QAL_INSPECTION_STD_DETR.*,PAY_EMPLOYEE_MST.EMP_NAME " & vbCrLf & " FROM QAL_INSPECTION_STD_DETR,PAY_EMPLOYEE_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_DETR.AUTO_KEY_STD=" & Val(lblMkey.Text) & "" & vbCrLf & " AND QAL_INSPECTION_STD_DETR.MOD_BY=PAY_EMPLOYEE_MST.EMP_CODE(+) " & vbCrLf & " AND PAY_EMPLOYEE_MST.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY QAL_INSPECTION_STD_DETR.SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionRev, ADODB.LockTypeEnum.adLockReadOnly)
        With RsInspectionRev
            If .EOF = True Then Exit Sub
            FormatSprdRev(-1)
            I = 1
            Do While Not .EOF
                SprdRev.Row = I

                SprdRev.Col = ColRevNo
                SprdRev.Text = Trim(IIf(IsDbNull(.Fields("REV_NO").Value), "", .Fields("REV_NO").Value))

                SprdRev.Col = ColModDet
                SprdRev.Text = Trim(IIf(IsDbNull(.Fields("MOD_DET").Value), "", .Fields("MOD_DET").Value))

                SprdRev.Col = ColModByCode
                SprdRev.Text = Trim(IIf(IsDbNull(.Fields("MOD_BY").Value), "", .Fields("MOD_BY").Value))

                SprdRev.Col = ColModByName
                SprdRev.Text = Trim(IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value))

                SprdRev.Col = ColDate
                SprdRev.Text = Trim(IIf(IsDbNull(.Fields("MOD_DATE").Value), "", .Fields("MOD_DATE").Value))

                .MoveNext()
                I = I + 1
                SprdRev.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtStandardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStandardNo.DoubleClick
        Call cmdSearchStndNo_Click(cmdSearchStndNo, New System.EventArgs())
    End Sub

    Private Sub txtStandardNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtStandardNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchStndNo_Click(cmdSearchStndNo, New System.EventArgs())
    End Sub

    Public Sub txtStandardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStandardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtStandardNo.Text) = "" Then GoTo EventExitSub
        txtStandardNo.Text = Trim(txtStandardNo.Text)
        If Len(txtStandardNo.Text) <= 6 Then
            txtStandardNo.Text = txtStandardNo.Text & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        mSlipNo = Val(txtStandardNo.Text)

        If MODIFYMode = True And RsInspectionMain.BOF = False Then xMkey = RsInspectionMain.Fields("AUTO_KEY_STD").Value

        SqlStr = "SELECT * FROM QAL_INSPECTION_STD_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_STD=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsInspectionMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_INSPECTION_STD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_STD=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspectionMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        '    cboStage.Enabled = mMode
        txtItemCode.Enabled = mMode
        cmdSearchICode.Enabled = mMode
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

    Private Sub ReportOnInspection(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mRs As ADODB.Recordset
        Dim mSource As String
        Dim mTitle As String


        SqlStr = "SELECT FIN_SUPP_CUST_MST.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_DET,FIN_SUPP_CUST_MST " & vbCrLf & " WHERE FIN_SUPP_CUST_DET.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND FIN_SUPP_CUST_DET.ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf & " AND FIN_SUPP_CUST_DET.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        With mRs
            If Not .EOF Then
                Do While Not .EOF
                    mSource = IIf(IsDbNull(.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
                    Report1.Reset()
                    MainClass.ClearCRptFormulas(Report1)

                    mTitle = "INSPECTION STANDARD"
                    SqlStr = "SELECT QAL_INSPECTION_STD_HDR.*,QAL_INSPECTION_STD_DET.*,INV_ITEM_MST.* " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,QAL_INSPECTION_STD_DET ,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=QAL_INSPECTION_STD_DET.AUTO_KEY_STD " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=" & Val(lblMkey.Text) & ""

                    Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspectionSTD.rpt"

                    MainClass.AssignCRptFormulas(Report1, "Source=""" & mSource & """")
                    MainClass.AssignCRptFormulas(Report1, "DocNo=""" & txtDocNo.Text & """")
                    MainClass.AssignCRptFormulas(Report1, "OrigDate=""" & txtDocDate.Text & """")
                    MainClass.AssignCRptFormulas(Report1, "RevNo=""" & txtRevNo.Text & """")
                    MainClass.AssignCRptFormulas(Report1, "RevDate=""" & txtRevDate.Text & """")

                    SetCrpt(Report1, Mode, 1, mTitle)
                    Report1.SQLQuery = SqlStr
                    Report1.WindowShowGroupTree = False

                    SqlStr1 = " SELECT QAL_INSPECTION_STD_DETR.*, PAY_EMPLOYEE_MST.EMP_NAME " & vbCrLf & " FROM QAL_INSPECTION_STD_DETR, PAY_EMPLOYEE_MST " & vbCrLf & " WHERE SUBSTR(AUTO_KEY_STD,LENGTH(AUTO_KEY_STD)-1,2) = PAY_EMPLOYEE_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_DETR.MOD_BY = PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND AUTO_KEY_STD =" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY REV_NO "

                    Report1.SubreportToChange = Report1.GetNthSubreportName(0)
                    Report1.Connect = STRRptConn
                    Report1.SQLQuery = SqlStr1

                    Report1.SubreportToChange = ""

                    Report1.Action = 1

                    .MoveNext()
                Loop
            Else
                mSource = "IN HOUSE"
                Report1.Reset()
                MainClass.ClearCRptFormulas(Report1)

                mTitle = "INSPECTION STANDARD"
                SqlStr = "SELECT QAL_INSPECTION_STD_HDR.*,QAL_INSPECTION_STD_DET.*,INV_ITEM_MST.* " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR,QAL_INSPECTION_STD_DET ,INV_ITEM_MST " & vbCrLf & " WHERE QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=QAL_INSPECTION_STD_DET.AUTO_KEY_STD " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_HDR.AUTO_KEY_STD=" & Val(lblMkey.Text) & ""

                Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspectionSTD.rpt"

                MainClass.AssignCRptFormulas(Report1, "Source=""" & mSource & """")
                MainClass.AssignCRptFormulas(Report1, "DocNo=""" & txtDocNo.Text & """")
                MainClass.AssignCRptFormulas(Report1, "OrigDate=""" & txtDocDate.Text & """")
                MainClass.AssignCRptFormulas(Report1, "RevNo=""" & txtRevNo.Text & """")
                MainClass.AssignCRptFormulas(Report1, "RevDate=""" & txtRevDate.Text & """")

                SetCrpt(Report1, Mode, 1, mTitle)
                Report1.SQLQuery = SqlStr
                Report1.WindowShowGroupTree = False

                SqlStr1 = " SELECT QAL_INSPECTION_STD_DETR.*, PAY_EMPLOYEE_MST.EMP_NAME " & vbCrLf & " FROM QAL_INSPECTION_STD_DETR, PAY_EMPLOYEE_MST " & vbCrLf & " WHERE SUBSTR(AUTO_KEY_STD,LENGTH(AUTO_KEY_STD)-1,2) = PAY_EMPLOYEE_MST.COMPANY_CODE " & vbCrLf & " AND QAL_INSPECTION_STD_DETR.MOD_BY = PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND AUTO_KEY_STD =" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY REV_NO "

                Report1.SubreportToChange = Report1.GetNthSubreportName(0)
                Report1.Connect = STRRptConn
                Report1.SQLQuery = SqlStr1

                Report1.SubreportToChange = ""

                Report1.Action = 1
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnInspection(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnInspection(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmInspectionStndProcess_InputLanguageChanged(sender As Object, e As InputLanguageChangedEventArgs) Handles Me.InputLanguageChanged

    End Sub
End Class
