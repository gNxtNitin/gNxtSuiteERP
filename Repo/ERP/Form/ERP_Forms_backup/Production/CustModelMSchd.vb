Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustModelMSchd
    Inherits System.Windows.Forms.Form
    Public Property pDetailValue As String

    Dim RsDSMain As ADODB.Recordset ''ADODB.Recordset										
    Dim RsDSDetail As ADODB.Recordset ''ADODB.Recordset										
    'Private PvtDBCn As ADODB.Connection										
    Dim mSearchStartRow As Integer

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14
    Dim mAmendSchd As Boolean

    Private Const ColModelCode As Short = 1
    Private Const ColModelDesc As Short = 2
    Private Const ColItemDetail As Short = 3
    Private Const ColWeek1Qty As Short = 4
    Private Const ColWeek2Qty As Short = 5
    Private Const ColWeek3Qty As Short = 6
    Private Const ColWeek4Qty As Short = 7
    Private Const ColWeek5Qty As Short = 8
    Private Const ColTotQty As Short = 9

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
            txtDSNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsDSMain.EOF = False Then RsDSMain.MoveFirst()
            Show1()
            txtDSNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume										
    End Sub

    Private Sub cmdAmendSchd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmendSchd.Click

        On Error GoTo ModifyErr

        '    If CDate(PubCurrDate) > CDate(txtScheduleDate.Text) Then										
        '        MsgInformation "MOnth Closed so Cann't be Modified."										
        '        Exit Sub										
        '    End If										

        ADDMode = False
        MODIFYMode = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtDSNo.Enabled = False
        txtDSAmendNo.Text = CStr(Val(txtDSAmendNo.Text) + 1)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtScheduleDate.Enabled = False
        cmdAmendSchd.Enabled = False
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendSchd = True
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtDSDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, (txtDSDate.Text), (txtSupplierName.Text)) = True Then
            Exit Sub
        End If
        '										
        '    If chkStatus.Value = vbChecked Then										
        '        MsgInformation "Posted DS Cann't be Deleted"										
        '        Exit Sub										
        '    End If										


        If Val(txtDSAmendNo.Text) > 0 Then
            MsgInformation("Amend DS Cann't be Deleted")
            Exit Sub
        End If

        If txtDSNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsDSMain.EOF Then
            If MainClass.ValidateWithMasterTable(Val(lblMkey.Text), "AUTO_KEY_REF", "AUTO_KEY_REF", "PPC_MODELWISE_MON_SCHD_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND POST_FLAG='Y'") = True Then
                MsgInformation("Posted DS Cann't be Deleted")
                Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.										
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PPC_MODELWISE_MON_SCHD_HDR", (txtDSNo.Text), RsDSMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PPC_MODELWISE_MON_SCHD_HDR", "AUTO_KEY_REF", (lblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PPC_MODELWISE_MON_SCHD_DET WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PPC_MODELWISE_MON_SCHD_HDR WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsDSMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsDSMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Posted DS Cann't be Modified")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtDSNo.Enabled = True

        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mDSNo As Double
        Dim mPostFlag As String
        Dim mScheduleStatus As String = "N"

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If mAmendSchd = True Then
            SqlStr = " INSERT INTO PPC_MODELWISE_MON_SCHDHIS_HDR SELECT * FROM PPC_MODELWISE_MON_SCHD_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_REF =" & Val(lblMkey.Text) & " AND AMEND_NO=" & Val(txtDSAmendNo.Text) - 1 & ""
            PubDBCn.Execute(SqlStr)

            SqlStr = " INSERT INTO PPC_MODELWISE_MON_SCHDHIS_DET  SELECT * FROM PPC_MODELWISE_MON_SCHD_DET WHERE AUTO_KEY_REF || AMEND_NO = (SELECT AUTO_KEY_REF || AMEND_NO FROM PPC_MODELWISE_MON_SCHD_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_REF =" & Val(lblMkey.Text) & " AND AMEND_NO=" & Val(txtDSAmendNo.Text) - 1 & ")"
            PubDBCn.Execute(SqlStr)

            SqlStr = " INSERT INTO PPC_MODELWISE_MON_SCHDHIS_TRN  SELECT * FROM PPC_MODELWISE_MON_SCHD_TRN WHERE AUTO_KEY_REF || AMEND_NO = (SELECT AUTO_KEY_REF || AMEND_NO FROM PPC_MODELWISE_MON_SCHD_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_REF =" & Val(lblMkey.Text) & " AND AMEND_NO=" & Val(txtDSAmendNo.Text) - 1 & ")"
            PubDBCn.Execute(SqlStr)
        End If
        mPostFlag = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""
        mDSNo = Val(txtDSNo.Text)
        If Val(txtDSNo.Text) = 0 Then
            mDSNo = AutoGenPONoSeq()
        End If
        txtDSNo.Text = CStr(mDSNo)

        If ADDMode = True Then
            lblMkey.Text = CStr(mDSNo)
            SqlStr = " INSERT INTO PPC_MODELWISE_MON_SCHD_HDR ( " & vbCrLf _
                & "  COMPANY_CODE , AUTO_KEY_REF," & vbCrLf _
                & "  REF_DATE , AMEND_NO," & vbCrLf _
                & "  AMEND_DATE , " & vbCrLf _
                & "  SUPP_CUST_CODE , PLAN_MONTH," & vbCrLf _
                & "  PLAN_STATUS," & vbCrLf _
                & "  REMARKS , POST_FLAG," & vbCrLf _
                & "  ADDUSER, ADDDATE, MODUSER, MODDATE) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mDSNo & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtDSAmendNo.Text) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mScheduleStatus & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & mPostFlag & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            'mScheduleStatus = lbl
            SqlStr = " UPDATE PPC_MODELWISE_MON_SCHD_HDR SET " & vbCrLf _
                & " AUTO_KEY_REF= " & mDSNo & "," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AMEND_NO=" & Val(txtDSAmendNo.Text) & ", " & vbCrLf _
                & " AMEND_DATE=TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " PLAN_MONTH=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PLAN_STATUS='" & mScheduleStatus & "'," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " POST_FLAG='" & mPostFlag & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_REF =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart
        If UpdateDailyDSDetail() = False Then GoTo ErrPart

        '    If UpdateProductWise = False Then GoTo ErrPart										

        Update1 = True
        PubDBCn.CommitTrans()
        txtDSNo.Text = CStr(mDSNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsDSMain.Requery()
        RsDSDetail.Requery()
        MsgBox(Err.Description)
        ''Resume										
    End Function
    Private Function UpdateDailyDSDetail() As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String
        Dim mModelCode As String

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColModelCode
                mModelCode = Trim(SprdMain.Text)
                SqlStr = "INSERT INTO PPC_MODELWISE_MON_SCHD_TRN (" & vbCrLf _
                & " COMPANY_CODE,AUTO_KEY_REF, SERIAL_NO, MODEL_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, ACTUAL_PLAN_QTY, AMEND_NO)" & vbCrLf _
                & " SELECT " & vbCrLf _
                & " COMPANY_CODE," & Val(txtDSNo.Text) & ", " & ii & ", MODEL_CODE, " & vbCrLf _
                & " SERIAL_DATE, PLANNED_QTY, " & vbCrLf _
                & " ACTUAL_PLAN_QTY, " & Val(txtDSAmendNo.Text) & " " & vbCrLf _
                & " FROM TEMP_MODELWISE_MON_SCHD_TRN " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(mModelCode) & "'" & " " '' & vbCrLf |                    & " AND PLAN_MONTH=TO_DATE('" & vb6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "')"										

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateDailyDSDetail = True
        Exit Function
UpdateErr1:
        UpdateDailyDSDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteDSDailyDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As Double) As Boolean
        Dim SqlStr As String
        On Error GoTo DeleteDSDailyDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM PPC_MODELWISE_MON_SCHD_TRN  " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(CStr(pMKey)) & " "
        pDBCn.Execute(SqlStr)
        DeleteDSDailyDetail = True
        Exit Function
DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteDSDailyDetail = False
    End Function
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Integer
        Dim SqlStr As String
        Dim mMaxValue As String
        mAutoGen = 1


        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = RsAutoGen.Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))    'CInt(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mModelCode As String
        Dim mItemUOM As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mTotQty As Double

        If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo UpdateDetail1

        SqlStr = "Delete From  PPC_MODELWISE_MON_SCHD_DET " & vbCrLf & " Where AUTO_KEY_REF=" & Val(lblMkey.Text) & "" & vbCrLf

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColModelCode
                mModelCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColWeek1Qty
                mWeek1Qty = Val(.Text)

                .Col = ColWeek2Qty
                mWeek2Qty = Val(.Text)

                .Col = ColWeek3Qty
                mWeek3Qty = Val(.Text)

                .Col = ColWeek4Qty
                mWeek4Qty = Val(.Text)

                .Col = ColWeek5Qty
                mWeek5Qty = Val(.Text)

                .Col = ColTotQty
                mTotQty = Val(.Text)

                SqlStr = ""
                If mModelCode <> "" Then ''And mTotQty > 0 ''If DS Amend Then Print ...										
                    SqlStr = " INSERT INTO PPC_MODELWISE_MON_SCHD_DET ( " & vbCrLf _
                        & " AUTO_KEY_REF, SERIAL_NO, MODEL_CODE, " & vbCrLf _
                        & " WEEK1_QTY, WEEK2_QTY, " & vbCrLf _
                        & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf _
                        & " WEEK5_QTY, TOTAL_QTY, AMEND_NO, " & vbCrLf _
                        & " COMPANY_CODE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf _
                        & " '" & mModelCode & "', " & vbCrLf _
                        & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf _
                        & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf _
                        & " " & mTotQty & "," & Val(txtDSAmendNo.Text) & ", " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ") "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume										
    End Function
    Private Function UpdateProductWise() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        'Dim SqlStr As String										
        Dim RsTemp As ADODB.Recordset
        Dim mModelCode As String
        Dim mProductCode As String
        Dim mPlanningQty As Double
        Dim mProdPlanNo As Double

        Dim SqlStr1 As String
        Dim RsTempDet As ADODB.Recordset

        Dim SqlStr2 As String
        Dim RsTempMonthly As ADODB.Recordset

        Dim mDeptCode As String
        Dim mDeptDesc As String

        Dim mSerialDate As String
        Dim mIPlanQty As Double
        Dim mActQty As Double
        Dim mItemSharing As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColModelCode
                mModelCode = Trim(.Text)

                .Col = ColTotQty
                mPlanningQty = Val(.Text)

                SqlStr = "SELECT ITEM_CODE FROM INV_MODELWISE_PROD_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MODEL_CODE='" & mModelCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                Do While Not RsTemp.EOF
                    mProductCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_MONTH_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                    PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                    PubDBCn.Execute("DELETE FROM PRD_PRODPLAN_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")


                    If MainClass.ValidateWithMasterTable(mProductCode, "ITEM_CODE", "OP_QTY", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo,  , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                        mItemSharing = Val(MasterNo)
                    Else
                        GoTo NextRec
                    End If

                    mProdPlanNo = AutoGenProdKeyNo()
                    SqlStr = " INSERT INTO PRD_PRODPLAN_HDR " & vbCrLf & " (AUTO_KEY_PRODPLAN ,COMPANY_CODE," & vbCrLf & " SUPP_CUST_CODE,PRODUCT_CODE,SCHLD_DATE," & vbCrLf & " CUST_ORD_QTY,REMARKS,PLAN_STATUS," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & mProdPlanNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "','" & MainClass.AllowSingleQuote(mProductCode) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPlanningQty * mItemSharing / 100)) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

                    PubDBCn.Execute(SqlStr)

                    SqlStr1 = " SELECT DISTINCT DEPT_CODE " & vbCrLf & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'" & vbCrLf & " START WITH  TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf & " CONNECT BY PRIOR (RM_CODE  || COMPANY_CODE)=PRODUCT_CODE || COMPANY_CODE "
                    MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTempDet.EOF = False Then
                        Do While Not RsTempDet.EOF
                            mDeptCode = IIf(IsDBNull(RsTempDet.Fields("DEPT_CODE").Value), "", RsTempDet.Fields("DEPT_CODE").Value)

                            If mDeptCode <> "J/W" Then
                                If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo,  , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                                    mDeptDesc = MasterNo
                                End If

                                SqlStr = " INSERT INTO PRD_PRODPLAN_DET ( " & vbCrLf & " AUTO_KEY_PRODPLAN,COMPANY_CODE,SUPP_CUST_CODE, " & vbCrLf & " PRODUCT_CODE,SCHLD_DATE,DEPT_CODE,DEPT_DESC,INHOUSE_CODE) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(CStr(mProdPlanNo)) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mProductCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mDeptCode & "', '" & mDeptDesc & "','" & MainClass.AllowSingleQuote(mProductCode) & "') "

                                PubDBCn.Execute(SqlStr)

                                SqlStr2 = "SELECT * FROM PPC_MODELWISE_MON_SCHD_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MODEL_CODE='" & mModelCode & "' AND TO_CHAR(SERIAL_DATE,'YYYYMM')='" & VB6.Format(txtScheduleDate.Text, "YYYYMM") & "' ORDER BY SERIAL_DATE"
                                MainClass.UOpenRecordSet(SqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempMonthly, ADODB.LockTypeEnum.adLockReadOnly)
                                If RsTempMonthly.EOF = False Then

                                    Do While Not RsTempMonthly.EOF
                                        mIPlanQty = IIf(IsDBNull(RsTempMonthly.Fields("PLANNED_QTY").Value), "", RsTempMonthly.Fields("PLANNED_QTY").Value)
                                        mIPlanQty = mIPlanQty * mItemSharing / 100
                                        mActQty = IIf(IsDBNull(RsTempMonthly.Fields("ACTUAL_PLAN_QTY").Value), "", RsTempMonthly.Fields("ACTUAL_PLAN_QTY").Value)
                                        mSerialDate = IIf(IsDBNull(RsTempMonthly.Fields("SERIAL_DATE").Value), "", RsTempMonthly.Fields("SERIAL_DATE").Value)

                                        SqlStr = "INSERT INTO PRD_PRODPLAN_MONTH_DET (" & vbCrLf & " AUTO_KEY_PRODPLAN,COMPANY_CODE,SUPP_CUST_CODE, " & vbCrLf & " PRODUCT_CODE,SCHLD_DATE,DEPT_CODE,SERIAL_DATE, " & vbCrLf & " IPLAN_QTY,DPLAN_QTY,ACTUAL_QTY,SB_RWK_QTY,NC_QTY,RWK_QTY,RWK_DONE_QTY,SCRAP_QTY,SEND_QTY,PROD_LOSS,PLAN_START,INHOUSE_CODE )" & vbCrLf & " VALUES (" & vbCrLf & " " & Val(CStr(mProdPlanNo)) & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mProductCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mDeptCode & "',TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & mIPlanQty & "," & mIPlanQty & "," & mActQty & ", " & vbCrLf & " 0,0,0, " & vbCrLf & " 0,0,0, " & vbCrLf & " '0','N','" & MainClass.AllowSingleQuote(mProductCode) & "') "
                                        PubDBCn.Execute(SqlStr)

                                        RsTempMonthly.MoveNext()
                                    Loop
                                End If
                            End If
                            RsTempDet.MoveNext()
                        Loop
                    End If
NextRec:
                    RsTemp.MoveNext()
                Loop
            Next
        End With



        UpdateProductWise = True
        Exit Function
UpdateDetail1:
        UpdateProductWise = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume										
    End Function

    Private Function AutoGenProdKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PRODPLAN)  " & vbCrLf & " FROM PRD_PRODPLAN_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PRODPLAN,LENGTH(AUTO_KEY_PRODPLAN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = RsAutoGen.Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenProdKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtSupplierName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplierName.Text = AcName
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmCustModelMSchd_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Customer Wise Model Wise Monthly Schedule"

        SqlStr = "Select * From PPC_MODELWISE_MON_SCHD_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PPC_MODELWISE_MON_SCHD_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " A.AUTO_KEY_REF AS RefNo, A.REF_DATE As DS_DATE, " & vbCrLf & " A.AMEND_NO AS Amendno, A.AMEND_DATE AS AmendDate,  " & vbCrLf & " B.SUPP_CUST_NAME AS NAME, " & vbCrLf & " A.PLAN_MONTH, DECODE(A.PLAN_STATUS,'N','OPEN','CLOSE') AS Status, " & vbCrLf & " A.REMARKS, DECODE(A.POST_FLAG,'Y','YES','NO') AS Posted " & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & " ORDER BY A.AUTO_KEY_REF"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmCustModelMSchd_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmCustModelMSchd_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection										
        'PvtDBCn.Open StrConn										
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

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

        mAccountCode = CStr(-1)
        lblMkey.Text = ""
        txtDSNo.Text = ""
        txtDSDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDSAmendNo.Text = CStr(0)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtSupplierName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True
        cmdsearch.Enabled = True
        SprdMain.Enabled = True

        txtScheduleDate.Text = "01/" & VB6.Format(Month(RunDate), "00") & "/" & VB6.Format(Year(RunDate), "0000")
        txtRemarks.Text = ""

        txtDSAmendNo.Enabled = False
        txtDSAmendDate.Enabled = False


        cmdAmendSchd.Enabled = False
        chkStatus.Enabled = True
        txtDSDate.Enabled = True
        txtScheduleDate.Enabled = True
        Call DelTemp_DailyDetail()
        mAmendSchd = False
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColModelCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDSDetail.Fields("MODEL_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 6)

            .Col = ColModelDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("MODEL_DESC", "GEN_MODEl_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 20)
            .TypeEditMultiLine = True

            .Col = ColItemDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False										
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColItemDetail, 6)

            .Col = ColWeek1Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK1_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .set_ColWidth(ColWeek1Qty, 8)


            .Col = ColWeek2Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK2_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek2Qty, 8)

            .Col = ColWeek3Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK3_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek3Qty, 8)

            .Col = ColWeek4Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK4_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek4Qty, 8)

            .Col = ColWeek5Qty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("WEEK5_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColWeek5Qty, 8)

            .Col = ColTotQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSDetail.Fields("TOTAL_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotQty, 9)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColModelDesc, ColModelDesc)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWeek1Qty, ColTotQty)
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
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 3500)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1000)
            .set_ColWidth(9, 2000)
            .set_ColWidth(10, 1000)
            .set_ColWidth(11, 1200)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtDSNo.MaxLength = RsDSMain.Fields("AUTO_KEY_REF").Precision
        txtDSDate.MaxLength = RsDSMain.Fields("REF_DATE").DefinedSize - 6
        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtRemarks.MaxLength = RsDSMain.Fields("REMARKS").DefinedSize

        txtDSAmendNo.MaxLength = RsDSMain.Fields("AMEND_NO").Precision
        txtDSAmendDate.MaxLength = RsDSMain.Fields("AMEND_DATE").DefinedSize - 6

        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsDSMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtScheduleDate.MaxLength = RsDSMain.Fields("PLAN_MONTH").DefinedSize - 6



        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume										
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mModelCode As String
        Dim mTotQty As Double
        Dim I As Integer
        Dim pDSNo As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtDSDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        '    If ValidateBookLocking(PubDBCn, ConLockPO_DS, txtDSDate) = True Then										
        '        FieldsVarification = False										
        '        Exit Function										
        '    End If										
        '										
        '    If ValidateAccountLocking(PubDBCn, txtScheduleDate.Text, txtSupplierName.Text) = True Then										
        '        FieldsVarification = False										
        '        Exit Function										
        '    End If										

        If MODIFYMode = True Then
            If mAmendSchd = False Then
                If RsDSMain.Fields("POST_FLAG").Value = "Y" And chkStatus.Enabled = False Then
                    MsgInformation("Posted DS Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If RsDSMain.Fields("PLAN_STATUS").Value = "Y" Then
                MsgInformation("Closed DS Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsDSMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtDSNo.Text) = "" Then
            MsgInformation("PO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtDSDate.Text) = "" Then
            MsgInformation(" PO Date is empty. Cannot Save")
            txtDSDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDSDate.Text) <> "" Then
            If IsDate(txtDSDate.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                txtDSDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Supplier Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            txtScheduleDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If DSExsistInCurrSchdMon((txtCode.Text), Trim(txtScheduleDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        ''28-09-2005										

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColModelCode
            mModelCode = Trim(UCase(SprdMain.Text))

            SprdMain.Col = ColTotQty
            mTotQty = Val(SprdMain.Text)

            If mModelCode <> "" And mTotQty > 0 Then
                If CheckDSDetailExists(mModelCode, I, mTotQty) = False Then
                    MsgInformation("Please Check Delivery Detail Qty. For Item Code :" & mModelCode)
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

        Next


        If MainClass.ValidDataInGrid(SprdMain, ColModelCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColModelDesc, "S", "Please Check Item Description.") = False Then FieldsVarification = False

        '    If MainClass.ValidDataInGrid(SprdMain, ColTotQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False										

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume										
    End Function
    Private Function CheckDSDetailExists(ByRef nItemCode As String, ByRef mSerialNo As Integer, ByRef mDSQty As Double) As Boolean

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset '' ADODB.Recordset										

        SqlStr = "SELECT SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf & " FROM TEMP_MODELWISE_MON_SCHD_TRN " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND MODEL_CODE='" & Trim(nItemCode) & "'" & vbCrLf & " GROUP BY MODEL_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("PLANNED_QTY").Value) = mDSQty Then
                CheckDSDetailExists = True
            Else
                CheckDSDetailExists = False
            End If
        Else
            CheckDSDetailExists = False
        End If
    End Function

    Private Sub frmCustModelMSchd_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsDSMain.Close()
        'RsOpOuts.Close										
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked

        Call ShowFormDSDailyDetail(eventArgs.col, eventArgs.row)

    End Sub

    Private Sub ShowFormDSDailyDetail(ByRef pCol As Integer, ByRef pRow As Integer)
        Dim I As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim pDate As String
        Dim mModelCode As String
        Dim mItemName As String
        Dim mQty As String

        With SprdMain
            .Row = pRow

            .Col = ColModelCode
            mModelCode = .Text
        End With
        If mModelCode = "" Then Exit Sub

        If Trim(txtScheduleDate.Text) = "" Then
            MsgInformation("Please Enter Valid Schedule Date")
            txtScheduleDate.Focus()
            Exit Sub
        End If
        Me.lblDetail.Text = pDetailValue     '' "False"

        With frmCustModelDailyDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblPoNo.Text = CStr(Val(txtDSNo.Text))
            .lblItemCode.Text = mModelCode
            .lblSuppCode.Text = txtCode.Text
            .LblPODate.Text = VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY")
            .lblMainActiveRow.Text = CStr(pRow)
            .ShowDialog()
        End With

        Me.lblDetail.Refresh()

        If Me.lblDetail.Text = pDetailValue Then     ''"True" Then
            With SprdMain
                .Row = pRow
                .Col = ColWeek1Qty
                .Text = CStr(Val(frmCustModelDailyDetail.lblWeek1.Text))
                .Col = ColWeek2Qty
                .Text = CStr(Val(frmCustModelDailyDetail.lblWeek2.Text))
                .Col = ColWeek3Qty
                .Text = CStr(Val(frmCustModelDailyDetail.lblWeek3.Text))
                .Col = ColWeek4Qty
                .Text = CStr(Val(frmCustModelDailyDetail.lblWeek4.Text))
                .Col = ColWeek5Qty
                .Text = CStr(Val(frmCustModelDailyDetail.lblWeek5.Text))
                frmCustModelDailyDetail.Close()
            End With
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call CalcTots()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mModelCode As String, ByRef mCol As Integer) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mModelCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = mCol
                If UCase(Trim(.Text)) = UCase(Trim(mModelCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Model Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColModelCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False										
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String



        If eventArgs.row = 0 And eventArgs.col = ColModelCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColModelCode
                If MainClass.SearchGridMaster(.Text, "GEN_MODEL_MST", "MODEL_CODE", "MODEL_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "MODEL_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColModelCode
                    .Text = Trim(AcName)
                    .Col = ColModelDesc
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColModelDesc And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColModelDesc
                If MainClass.SearchGridMaster(.Text, "GEN_MODEL_MST", "MODEL_DESC", "MODEL_CODE",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "MODEL_DESC") = True Then
                    .Row = .ActiveRow
                    .Col = ColModelDesc
                    .Text = Trim(AcName)
                    .Col = ColModelCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColModelDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColModelCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModelCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColModelDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModelDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xModelDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColModelCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColModelCode
                xModelDesc = SprdMain.Text
                If xModelDesc = "" Then Exit Sub


                If GetValidModel(xModelDesc, True) = True Then
                    If CheckDuplicateItem(xModelDesc, ColModelCode) = False Then
                        If FillGridRow(xModelDesc, True) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColModelDesc, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColModelDesc)
                End If
            Case ColModelDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColModelDesc
                xModelDesc = SprdMain.Text
                If xModelDesc = "" Then Exit Sub


                If GetValidModel(xModelDesc, False) = True Then
                    If CheckDuplicateItem(xModelDesc, ColModelDesc) = False Then
                        If FillGridRow(xModelDesc, False) = False Then Exit Sub
                        MainClass.AddBlankSprdRow(SprdMain, ColModelDesc, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColModelDesc)
                End If

                '        Case ColTotQty										
                '            If CheckItemRate() = True Then										
                '                MainClass.AddBlankSprdRow SprdMain, ColModelDesc, ConRowHeight										
                '                FormatSprdMain -1										
                '            End If										
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColModelCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColTotQty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MsgInformation("Please Enter the Qty.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColTotQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mModelDesc As String, ByRef mIsModelCode As Boolean) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String

        If mModelDesc = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select MODEL_CODE,MODEL_DESC" & vbCrLf & " FROM GEN_MODEL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mIsModelCode = True Then
            SqlStr = SqlStr & vbCrLf & " AND MODEL_CODE='" & Trim(mModelDesc) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND MODEL_DESC='" & Trim(mModelDesc) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColModelCode
                SprdMain.Text = IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value)

                SprdMain.Col = ColModelDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("MODEL_DESC").Value), "", .Fields("MODEL_DESC").Value)
            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtDSNo.Text = SprdView.Text

        txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtDSAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", vbInformation)
            Cancel = True
            Exit Sub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtScheduleDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScheduleDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtScheduleDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            Cancel = True
        End If


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplierName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplierName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplierName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim xAcctCode As String

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", vbInformation)
            Cancel = True
            Exit Sub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""

        Clear1()
        If Not RsDSMain.EOF Then

            lblMkey.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_REF").Value), "", RsDSMain.Fields("AUTO_KEY_REF").Value)
            txtDSNo.Text = IIf(IsDBNull(RsDSMain.Fields("AUTO_KEY_REF").Value), "", RsDSMain.Fields("AUTO_KEY_REF").Value)
            txtDSDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("REF_DATE").Value), "", RsDSMain.Fields("REF_DATE").Value), "DD/MM/YYYY")
            txtDSAmendNo.Text = IIf(IsDBNull(RsDSMain.Fields("AMEND_NO").Value), 0, RsDSMain.Fields("AMEND_NO").Value)
            txtDSAmendDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("AMEND_DATE").Value), "", RsDSMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")

            txtRemarks.Text = IIf(IsDBNull(RsDSMain.Fields("REMARKS").Value), "", RsDSMain.Fields("REMARKS").Value)

            chkStatus.CheckState = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            cmdAmendSchd.Enabled = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", True, False)

            'If RsDSMain.Fields("POST_FLAG").Value = "Y" Then
            '    cmdAmendSchd.Enabled = False
            'End If

            chkStatus.Enabled = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)

            txtScheduleDate.Text = VB6.Format(IIf(IsDBNull(RsDSMain.Fields("PLAN_MONTH").Value), "", RsDSMain.Fields("PLAN_MONTH").Value), "DD/MM/YYYY")
            txtScheduleDate.Enabled = IIf(RsDSMain.Fields("POST_FLAG").Value = "Y", False, True)


            mAccountCode = IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), -1, RsDSMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsDSMain.Fields("SUPP_CUST_CODE").Value), "", RsDSMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = False
            cmdsearch.Enabled = False
            mAmendSchd = False
            Call ShowDetail1()
            Call ShowDSDailyDetail()
        End If
        Call CalcTots()
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColModelDesc, ColModelDesc)
        MainClass.ButtonStatus(Me, XRIGHT, RsDSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume										
    End Sub
    Private Sub ShowDSDailyDetail()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String

        Call DelTemp_DailyDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_MODELWISE_MON_SCHD_TRN ( " & vbCrLf _
            & " UserId, COMPANY_CODE,AUTO_KEY_REF, SERIAL_NO,MODEL_CODE, " & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_PLAN_QTY)" & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", AUTO_KEY_REF, SERIAL_NO,MODEL_CODE," & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_PLAN_QTY" & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_TRN " & vbCrLf _
            & " WHERE AUTO_KEY_REF=" & Val(lblMkey.Text) & " " & vbCrLf _
            & " ORDER BY SERIAL_NO, SERIAL_DATE"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_DailyDetail(Optional ByRef mRefNo As String = "", Optional ByRef mModelCode As String = "")

        Dim SqlStr As String

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_MODELWISE_MON_SCHD_TRN " & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mRefNo <> "" And mModelCode <> "" Then
            SqlStr = SqlStr & "AND AUTO_KEY_REF=" & Val(mRefNo) & "' " & "AND MODEL_CODE='" & MainClass.AllowSingleQuote(UCase(mModelCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mModelCode As String
        Dim mItemDesc As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_DET " & vbCrLf _
            & " Where AUTO_KEY_REF=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1										
            I = 1
            '        .MoveFirst										

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColModelCode
                mModelCode = Trim(IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value))
                SprdMain.Text = mModelCode

                SprdMain.Col = ColModelDesc
                MainClass.ValidateWithMasterTable(mModelCode, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColWeek1Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK1_QTY").Value), 0, .Fields("WEEK1_QTY").Value)))

                SprdMain.Col = ColWeek2Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK2_QTY").Value), 0, .Fields("WEEK2_QTY").Value)))

                SprdMain.Col = ColWeek3Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK3_QTY").Value), 0, .Fields("WEEK3_QTY").Value)))

                SprdMain.Col = ColWeek4Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK4_QTY").Value), 0, .Fields("WEEK4_QTY").Value)))

                SprdMain.Col = ColWeek5Qty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("WEEK5_QTY").Value), 0, .Fields("WEEK5_QTY").Value)))

                SprdMain.Col = ColTotQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("TOTAL_QTY").Value), 0, .Fields("TOTAL_QTY").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume										
    End Sub
    Private Sub txtDSDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtDSNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDSNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String
        Dim mDSNo As Double
        Dim SqlStr As String

        If Trim(txtDSNo.Text) = "" Then GoTo EventExitSub

        If Len(txtDSNo.Text) < 6 Then
            txtDSNo.Text = Val(txtDSNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        mDSNo = Val(txtDSNo.Text)

        If MODIFYMode = True And RsDSMain.BOF = False Then xMkey = RsDSMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "SELECT * FROM PPC_MODELWISE_MON_SCHD_HDR " & " WHERE AUTO_KEY_REF='" & MainClass.AllowSingleQuote(UCase(CStr(mDSNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PPC_MODELWISE_MON_SCHD_HDR WHERE AUTO_KEY_REF=" & Val(xMkey) & "" & vbCrLf _
                    & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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





    Private Function GetValidModel(ByRef pModelDesc As String, ByRef mIsCode As Boolean) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset


        If mIsCode = True Then
            If MainClass.ValidateWithMasterTable(pModelDesc, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetValidModel = True
                Exit Function
            Else
                MsgInformation("Invalid Model.")
                GetValidModel = False
                Exit Function
            End If
        Else
            If MainClass.ValidateWithMasterTable(pModelDesc, "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetValidModel = True
                Exit Function
            Else
                MsgInformation("Invalid Model.")
                GetValidModel = False
                Exit Function
            End If
        End If


        Exit Function
ErrPart:
        GetValidModel = False
    End Function
    Private Function SelectQryForDS(ByRef mSqlStr As String) As String

        ''''SELECT CLAUSE...										

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''''FROM CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR IH, PPC_MODELWISE_MON_SCHD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''''WHERE CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REF=" & Val(txtDSNo.Text) & ""

        ''''ORDER CLAUSE...										

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDS = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))										
        '										
        '    MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"										
        '    MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"										

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnDS(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String
        Dim mVNo As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        '    Call SelectQryForDS(SqlStr)										
        '    mTitle = "Delivery Schedule"										
        '    mRptFileName = "DS.rpt"										
        '										
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)										
        '										
        '    Response = MsgQuestion("Do You Want to Print Detail Delivery Schedule?")										
        '										
        '    If Response = vbYes Then										
        '        Call MainClass.ClearCRptFormulas(Report1)										
        '										
        '        Call SelectQryForDailyDS(SqlStr)										
        '        mTitle = "Shortage Follow-up register for the month of " & vb6.Format(txtScheduleDate, "MMMM , YYYY")										
        '        mRptFileName = "DSDetail.rpt"										
        '										
        '        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)										
        '    End If										

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDailyDS(ByRef mSqlStr As String) As String

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"

        ''''FROM CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM PPC_MODELWISE_MON_SCHD_HDR IH, PPC_MODELWISE_MON_SCHD_TRN ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''''WHERE CLAUSE...										
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_REF=" & Val(txtDSNo.Text) & "" & vbCrLf

        ''''ORDER CLAUSE...										

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        SelectQryForDailyDS = mSqlStr

    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDS(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Function DSExsistInCurrSchdMon(ByRef pSuppCustCode As String, ByRef pSchdDate As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim xDSNo As Double
        Dim pDSNo As Double

        pDSNo = Val(txtDSNo.Text)

        SqlStr = "SELECT AUTO_KEY_REF " & vbCrLf & " FROM PPC_MODELWISE_MON_SCHD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCustCode & "'" & vbCrLf & " AND TO_CHAR(PLAN_MONTH,'MM-YYYY')=TO_CHAR('" & VB6.Format(pSchdDate, "MM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & "AND AUTO_KEY_REF<>" & pDSNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xDSNo = RsTemp.Fields("AUTO_KEY_REF").Value
            MsgInformation("Plan (" & xDSNo & ") Already Made in this Month for Such Customer.")
            DSExsistInCurrSchdMon = True
        Else
            DSExsistInCurrSchdMon = False
        End If

        Exit Function
ErrPart:
        DSExsistInCurrSchdMon = True
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset										
        Dim mGrossQty As Double

        Dim I As Integer
        Dim j As Integer


        mGrossQty = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                mGrossQty = 0

                .Col = ColWeek1Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek2Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek3Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek4Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColWeek5Qty
                mGrossQty = mGrossQty + Val(.Text)

                .Col = ColTotQty
                .Text = CStr(Val(CStr(mGrossQty)))

            Next I
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume										
    End Sub
End Class
