Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmCRRework
    Inherits System.Windows.Forms.Form
    Dim RsPMemoMain As ADODB.Recordset ''Recordset	
    Dim RsPMemoDetail As ADODB.Recordset ''Recordset	
    Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUom As Short = 3
    Private Const ColRate As Short = 4
    Private Const ColReWorkQty As Short = 5
    Private Const ColAmount As Short = 6
    Private Const ColOPR As Short = 7
    Private Const ColOPRERCode As Short = 8
    Private Const ColStockType As Short = 9
    Private Const ColToolNo As Short = 10
    Private Const ColReason As Short = 11
    Private Const ColCostPcs As Short = 12
    Private Function GetDevelopmentItemProdQty(ByRef xProductCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetDevelopmentItemProdQty = 0

        SqlStr = " SELECT SUM(PROD_QTY) AS PROD_QTY " & vbCrLf _
            & " FROM PRD_REWORK_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDevelopmentItemProdQty = IIf(IsDbNull(RsTemp.Fields("PROD_QTY").Value), 0, RsTemp.Fields("PROD_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetDevelopmentItemProdQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset = Nothing	
        Dim mQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mNetAmount As Object

        Dim i As Integer
        Dim j As Integer

        mNetAmount = 0

        With SprdMain
            j = .MaxRows
            For i = 1 To j
                .Row = i

                .Col = ColItemCode
                If Trim(.Text) <> "" Then
                    .Col = ColRate
                    mRate = Val(.Text)

                    .Col = ColReWorkQty
                    mQty = Val(.Text)

                    mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))

                    .Col = ColAmount
                    .Text = CStr(mAmount)

                    mNetAmount = mNetAmount + mAmount
                End If
            Next i
        End With

        lblMaterialCost.Text = VB6.Format(mNetAmount, "#0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboShiftcd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShiftcd.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShiftcd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboShiftcd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(cboShiftcd.Text) = "C" Then
            If Trim(txtPMemoDate.Text) <> "" Then
                txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
                txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
            End If
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(True))
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillCbo()

        On Error GoTo FillERR

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1
        cboShiftcd.Items.Clear()
        cboShiftcd.Items.Add(("A"))
        cboShiftcd.Items.Add(("B"))
        cboShiftcd.Items.Add(("C"))

        cboShiftcd.SelectedIndex = 0

        cboType.Items.Clear()
        '    cboType.AddItem "Rework"	
        '    cboType.AddItem "Pre-Despatch Rework"	
        cboType.Items.Add("Customer Rejection")

        cboType.SelectedIndex = 0

        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mItemCode As String

        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            Exit Sub
        End If
        If Trim(txtPMemoNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Voucher Already Approved, So cann't be Delete.")
            Exit Sub
        End If

        If Not RsPMemoMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_REWORK_HDR ", (txtPMemoNo.Text), RsPMemoMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_REWORK_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart
                If DeleteCRTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                If DeleteStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PRD_REWORK_DET WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM PRD_REWORK_HDR  WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'")
                PubDBCn.CommitTrans()
                RsPMemoMain.Requery()
                RsPMemoDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If cmdModify.Text = ConcmdmodifyCaption Then

            If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked And PubUserID <> "G0416" Then
                MsgInformation("Voucher Already Approved, So cann't be Modify.")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtPMemoNo.Enabled = False
            cmdSearch.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mSlipNo As String
        Dim mRPTName As String
        mSlipNo = CStr(Val(txtPMemoNo.Text))

        Report1.Reset()
        '    SqlStr = "SELECT * " & vbCrLf _	
        ''            & " FROM PRD_SENDBACKFORRWK_HDR IH, PRD_SENDBACKFORRWK_DET ID, INV_ITEM_MST INVMST, PAY_EMPLOYEE_MST EMP" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND IH.AUTO_KEY_SBRWK=ID.AUTO_KEY_SBRWK" & vbCrLf _	
        ''            & " AND IH.COMPANY_CODE=EMP.COMPANY_CODE(+)" & vbCrLf _	
        ''            & " AND IH.SHIFT_EMP_CODE=EMP.EMP_CODE(+)" & vbCrLf _	
        ''            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
        ''            & " AND ID.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf _	
        ''            & " AND IH.AUTO_KEY_SBRWK=" & mSlipNo & ""	

        mTitle = "Customer Rejection Rework"
        mSubTitle = ""
        mRPTName = "CRRework.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRPTName)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	

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

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "'"

        If lblShow.Text = "R" Then
            SqlStr = SqlStr & " AND RECD_QTY=0"
        End If

        If MainClass.SearchGridMaster(txtPMemoNo.Text, "PRD_REWORK_HDR ", "AUTO_KEY_REF", "PRODUCT_CODE", "DEPT_CODE", "SEND_DEPT_CODE", SqlStr) = True Then
            txtPMemoNo.Text = AcName
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDept.text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmp.text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub cmdSearchProductCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProductCode.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtProductCode.Text = AcName1
            lblProductCode.text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmCRRework_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim pOPRCode As String
        Dim mProductCode As String
        Dim pOPRDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And Trim(txtProductCode.Text) <> "" Then
            With SprdMain
                mProductCode = Trim(txtProductCode.Text)
                .Row = .ActiveRow

                .Col = ColItemCode

                SqlStr = " SELECT DISTINCT TRN.RM_CODE, INVMST.ITEM_SHORT_DESC " & vbCrLf _
                    & " FROM VW_PRD_BOM_TRN TRN, INV_ITEM_MST INVMST " & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TRN.STATUS='O' AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


                SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.RM_CODE=INVMST.ITEM_CODE"

                SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(TRN.PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(TRN.RM_CODE) || TRN.COMPANY_CODE || ' ')=TRIM(TRN.PRODUCT_CODE) || TRN.COMPANY_CODE || ' '"

                SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY TRN.RM_CODE"


                'AGNCHKCODE:	
                '            SqlStr = "SELECT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC " & vbCrLf _	
                ''                    & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                    & " AND IH.STATUS='O'" & vbCrLf _	
                ''                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                    & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                    & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                '	
                '            SqlStr = SqlStr & vbCrLf & " UNION "	
                '	
                '            SqlStr = SqlStr & vbCrLf & "SELECT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC " & vbCrLf _	
                ''                    & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, PRD_BOM_ALTER_DET AD, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                    & " AND IH.STATUS='O'" & vbCrLf _	
                ''                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                    & " AND ID.MKEY=AD.MKEY" & vbCrLf _	
                ''                    & " AND ID.RM_CODE=AD.MAINITEM_CODE" & vbCrLf _	
                ''                    & " AND ID.SUBROWNO=AD.MAINSUBROWNO" & vbCrLf _	
                ''                    & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                    & " AND AD.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                '	
                '            SqlStr = SqlStr & vbCrLf & " ORDER BY 1"	
                '	
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
                '	
                '            If RsTemp.EOF = True Then	
                '                SqlStr = "SELECT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC " & vbCrLf _	
                ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                        & " AND IH.STATUS='O'" & vbCrLf _	
                ''                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                        & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                        & " AND ID.RM_CODE=INVMST.ITEM_CODE "	
                '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
                '	
                '                If RsTemp.EOF = False Then	
                '                    mProductCode = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)	
                '                    GoTo AGNCHKCODE	
                '                Else	
                '                    Exit Sub	
                '                End If	
                '            End If	

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc And Trim(txtProductCode.Text) <> "" Then
            With SprdMain
                mProductCode = Trim(txtProductCode.Text)
                .Row = .ActiveRow

                .Col = ColItemDesc
                SqlStr = " SELECT DISTINCT INVMST.ITEM_SHORT_DESC , TRN.RM_CODE" & vbCrLf & " FROM VW_PRD_BOM_TRN TRN, INV_ITEM_MST INVMST " & vbCrLf & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TRN.STATUS='O' AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

                SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND TRN.RM_CODE=INVMST.ITEM_CODE"

                SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(TRN.PRODUCT_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(TRN.RM_CODE) || TRN.COMPANY_CODE || ' ')=TRIM(TRN.PRODUCT_CODE) || TRN.COMPANY_CODE || ' '"

                SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY INVMST.ITEM_SHORT_DESC"

                'AGNCHKDESC:	
                '            SqlStr = "SELECT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                    & " AND IH.STATUS='O'" & vbCrLf _	
                ''                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                    & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                    & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                '	
                '            SqlStr = SqlStr & vbCrLf & " UNION "	
                '	
                '            SqlStr = SqlStr & vbCrLf & "SELECT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, PRD_BOM_ALTER_DET AD, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                    & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                    & " AND IH.STATUS='O'" & vbCrLf _	
                ''                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                    & " AND ID.MKEY=AD.MKEY" & vbCrLf _	
                ''                    & " AND ID.RM_CODE=AD.MAINITEM_CODE" & vbCrLf _	
                ''                    & " AND ID.SUBROWNO=AD.MAINSUBROWNO" & vbCrLf _	
                ''                    & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                    & " AND AD.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _	
                ''                    & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                '	
                '            SqlStr = SqlStr & vbCrLf & " ORDER BY 1"	
                '	
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
                '	
                '            If RsTemp.EOF = True Then	
                '                SqlStr = "SELECT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC " & vbCrLf _	
                ''                        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _	
                ''                        & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _	
                ''                        & " AND IH.STATUS='O'" & vbCrLf _	
                ''                        & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
                ''                        & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _	
                ''                        & " AND ID.RM_CODE=INVMST.ITEM_CODE "	
                '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
                '	
                '                If RsTemp.EOF = False Then	
                '                    mProductCode = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)	
                '                    GoTo AGNCHKDESC	
                '                Else	
                '                    Exit Sub	
                '                End If	
                '            End If	

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                    Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, False))
                End If

            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColStockType
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColStockType, .ActiveRow, ColStockType, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOPR Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                mProductCode = Trim(.Text)

                .Col = ColOPR
                SqlStr = OperationQuery(Trim(mProductCode), Trim(txtDept.Text), "", Trim(.Text), Trim(txtPMemoDate.Text), "TRIM(TO_CHAR(OPR_SNO,'00')) || '-' || MST.OPR_DESC", "TRN.OPR_CODE", "TO_CHAR(OPR_SNO)")

                '            SqlStr = " SELECT TRIM(TO_CHAR(OPR_SNO,'00')) || '-' || MST.OPR_DESC, TRN.OPR_CODE, TO_CHAR(OPR_SNO) " & vbCrLf _	
                ''                    & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
                ''                    & " WHERE " & vbCrLf _	
                ''                    & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
                ''                    & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
                ''                    & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
                '	
                '            If Trim(.Text) <> "" Then	
                '                SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"	
                '            End If	
                '	
                '            If Trim(mProductCode) <> "" Then	
                '                SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
                '            End If	
                '	
                '            SqlStr = SqlStr & vbCrLf & " ORDER BY OPR_SNO"	

                '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPR
                    .Text = Trim(Mid(AcName, 4))
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPR, .ActiveRow, ColOPR, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOPRERCode Then
            With SprdMain
                .Row = .ActiveRow

                '            .Col = ColItemCode	
                '            mProductCode = Trim(.Text)	

                .Col = ColOPRERCode
                SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"


                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"
                End If
                '            SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"	

                SqlStr = SqlStr & vbCrLf & " UNION "

                SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "' AND EMP_TYPE='W'"


                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                '            SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"	

                '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRERCode
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRERCode, .ActiveRow, ColOPRERCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColToolNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColOPR
                pOPRDesc = Trim(.Text)

                SqlStr = OperationQuery(Trim(txtProductCode.Text), Trim(txtDept.Text), "", Trim(pOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

                '            SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
                ''                    & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
                ''                    & " WHERE " & vbCrLf _	
                ''                    & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
                ''                    & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
                ''                    & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
                ''                    & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"	

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    pOPRCode = IIf(IsDBNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                Else
                    pOPRCode = "-1"
                End If

                .Col = ColToolNo
                If MainClass.SearchGridMaster(.Text, "TOL_TOOLINFO_MST", "TOOL_NO", "OPR_CODE", "DEPT_CODE", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OPR_CODE='" & MainClass.AllowSingleQuote(pOPRCode) & "' AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then
                    .Row = .ActiveRow

                    .Col = ColToolNo
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColToolNo, .ActiveRow, ColToolNo, .ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDivisionCode As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If DuplicateItem() = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text)) = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColStockType
                Call CheckStockType()
            Case ColOPR
                If DuplicateItem() = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOPR)
                    eventArgs.cancel = True
                    Exit Sub
                End If
                Call CheckOPR()
            Case ColOPRERCode
                If DuplicateItem() = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOPRERCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
                Call CheckOPERATOR()
            Case ColToolNo
                Call CheckToolNo()
            Case ColReWorkQty
                Call CheckItemReworkQty((SprdMain.ActiveRow))
        End Select

        Call CalcTots()
        Exit Sub

ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mCheckOPR As String
        Dim mCheckOperatorCode As String
        Dim mItemCode As String
        Dim mOPR As String
        Dim mOperatorCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColOPR
            mCheckOPR = Trim(UCase(.Text))

            .Col = ColOPRERCode
            mCheckOperatorCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColOPR
                mOPR = Trim(UCase(.Text))

                .Col = ColOPRERCode
                mOperatorCode = Trim(UCase(.Text))

                If (mCheckItemCode & "-" & mCheckOPR & "-" & mCheckOperatorCode = mItemCode & "-" & mOPR & "-" & mOperatorCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode & " Operation : " & mCheckOPR & " Operator : " & mCheckOperatorCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Sub CheckStockType()

        On Error GoTo ChkERR
        Dim mStockType As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColStockType
            If Trim(.Text) = "" Then Exit Sub
            If MainClass.ValidateWithMasterTable(Trim(.Text), "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mStockType = MasterNo
                '            If Trim(mStockType) <> "FG" Then	
                '                MsgInformation "Please Select 'FG' Stock Type."	
                '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColStockType	
                '                Exit Sub	
                '            End If	
            Else
                MsgInformation("Invalid Stock Type.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColStockType)
                Exit Sub
            End If
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub


    Private Function CheckItemReworkQty(ByRef mRowNo As Integer) As Boolean

        On Error GoTo ChkERR
        Dim mReworkQty As Double
        Dim mReqdReworkQty As Double
        Dim mItemCode As String
        Dim mUOM As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mProductCode As String

        CheckItemReworkQty = False
        mProductCode = Trim(txtProductCode.Text)
        With SprdMain
            .Row = mRowNo
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function
            mItemCode = Trim(.Text)

            .Col = ColReWorkQty
            mReworkQty = Val(.Text)

            .Col = ColUom
            mUOM = Trim(.Text)

            mReqdReworkQty = 0

            SqlStr = " SELECT (STD_QTY + GROSS_WT_SCRAP) AS STD_QTY, PRODUCT_CODE,RM_CODE " & vbCrLf _
                & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
                & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE<>'J/W' AND RM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

            SqlStr = SqlStr & vbCrLf _
                    & " START WITH  TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' AND DEPT_CODE<>'J/W'" & vbCrLf _
                    & " CONNECT BY PRIOR RM_CODE=PRODUCT_CODE AND DEPT_CODE<>'J/W'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mProductCode = Trim(IIf(IsDbNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                    If CheckBOMForItem(mProductCode, mItemCode) = True Then
                        mReqdReworkQty = IIf(IsDbNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value)
                        '                If mUOM = "KGS" Then	
                        '                   mReqdReworkQty = mReqdReworkQty / 1000	
                        '                ElseIf mUOM = "TON" Or mUOM = "MT" Then	
                        '                   mReqdReworkQty = mReqdReworkQty / 1000	
                        '                   mReqdReworkQty = mReqdReworkQty / 1000	
                        '                End If	


                        mReqdReworkQty = mReqdReworkQty * Val(txtReWorkQty.Text)

                        If mReworkQty > mReqdReworkQty Then
                            CheckItemReworkQty = False
                            MsgInformation("Rework Qty is more than Required Qty, So cann't be Save.")
                            MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColReWorkQty)
                            Exit Function
                        End If
                        CheckItemReworkQty = True
                        Exit Function
                    End If
                    RsTemp.MoveNext()
                Loop

                CheckItemReworkQty = False
                MsgInformation("Invaild Item Code For Product Code " & Trim(txtProductCode.Text) & ", So cann't be Save.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                Exit Function

            Else
                CheckItemReworkQty = False
                MsgInformation("Invaild Item Code For Product Code " & Trim(txtProductCode.Text) & ", So cann't be Save.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                Exit Function
            End If

        End With
        CheckItemReworkQty = True
        Exit Function
ChkERR:
        CheckItemReworkQty = False
        MsgBox(Err.Description)
    End Function
    Private Sub CheckOPR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProductCode As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColItemCode
            mProductCode = Trim(.Text)

            .Col = ColOPR
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = OperationQuery(Trim(txtProductCode.Text), Trim(txtDept.Text), "", Trim(.Text), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

            '        SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
            ''                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
            ''                & " WHERE " & vbCrLf _	
            ''                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
            ''                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
            ''                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
            ''                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"	
            '	
            '        If Trim(mProductCode) <> "" Then	
            '            SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"	
            '        End If	

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operation for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOPR)
                Exit Sub
            End If

            '        If MainClass.ValidateWithMasterTable(Trim(.Text), "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then	
            '            mOPR = MasterNo	
            '        Else	
            '            MsgInformation "Invalid Operation for such Dept."	
            '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColOPR	
            '            Exit Sub	
            '        End If	
        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckOPERATOR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mProductCode As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColOPRERCode
            If Trim(.Text) = "" Then Exit Sub

            SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf _
                    & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
                    & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            If ADDMode = True Then
                SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"
            End If

            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            SqlStr = SqlStr & vbCrLf & " UNION "

            SqlStr = SqlStr & vbCrLf & " SELECT EMP_NAME, EMP_CODE " & vbCrLf _
                    & " FROM PAY_EMPLOYEE_MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND EMP_CODE='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
                    & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

            SqlStr = SqlStr & vbCrLf & " AND EMP_TYPE='W'"


            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operator Name for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOPRERCode)
                Exit Sub
            End If

        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckUnapprovedMRR() As Boolean

        On Error GoTo ChkERR
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim pRefNo As String

        CheckUnapprovedMRR = False

        SqlStr = " SELECT AUTO_KEY_REF " & vbCrLf & " FROM PRD_REWORK_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND APPROVED='N' AND AUTO_KEY_REF<>" & Val(txtPMemoNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            pRefNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REF").Value), "", RsTemp.Fields("AUTO_KEY_REF").Value)
            MsgInformation("Ref No : " & pRefNo & " is not Approved. Please approved this Ref No.")
            CheckUnapprovedMRR = True
            Exit Function
        End If



        Exit Function
ChkERR:
        CheckUnapprovedMRR = True
        MsgBox(Err.Description)
    End Function
    Private Sub CheckToolNo()

        On Error GoTo ChkERR
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        With SprdMain
            .Row = .ActiveRow

            .Col = ColOPR
            pOPRDesc = Trim(.Text)

            SqlStr = OperationQuery(Trim(txtProductCode.Text), Trim(txtDept.Text), "", Trim(pOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

            '        SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
            ''                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
            ''                & " WHERE " & vbCrLf _	
            ''                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
            ''                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
            ''                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
            ''                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
            ''                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"	

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
            Else
                pOPRCode = "-1"
            End If

            .Col = ColToolNo
            If Trim(.Text) = "" Then Exit Sub

            If MainClass.ValidateWithMasterTable(Trim(.Text), "TOOL_NO", "TOOL_NO", "TOL_TOOLINFO_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND OPR_CODE='" & MainClass.AllowSingleQuote(pOPRCode) & "' AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = False Then
                MsgInformation("Invalid Tool No.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColToolNo)
                Exit Sub
            End If
        End With

        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckQty() As Boolean
        On Error GoTo ERR1
        Dim mProdQty As Double

        CheckQty = True
        Exit Function

        With SprdMain
            .Row = .ActiveRow
            '        .Col = ColProdQty	
            '        mProdQty = Val(.Text)	
            '	
            '        .Col = ColOKQty	
            '        mOKQty = Val(.Text)	
            '	
            '        If mProdQty < mOKQty Then	
            '            CheckQty = False	
            '        Else	
            '            CheckQty = True	
            '        End If	
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescPart(ByRef pItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset = Nothing	
        Dim mProductCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String
        Dim mRate As String

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function
        If Trim(txtProductCode.Text) = "" Then FillItemDescPart = False : Exit Function
        If Trim(txtDept.Text) = "" Then FillItemDescPart = False : Exit Function

        mProductCode = Trim(txtProductCode.Text)
        '	
        '    SqlStr = " SELECT PRODUCT_CODE,RM_CODE " & vbCrLf _	
        ''                & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _	
        ''                & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"	
        '	
        '    SqlStr = SqlStr & vbCrLf _	
        ''            & " START WITH PRODUCT_CODE || '-' || TRN.COMPANY_CODE ='" & MainClass.AllowSingleQuote(mProductCode) & "-" & RsCompany.fields("COMPANY_CODE").value & "'" & vbCrLf _	
        ''            & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"	
        '	
        '	
        '	
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
        '	
        '    If RsTemp.EOF = False Then	
        '        Do While RsTemp.EOF = False	
        '            mProductCode = Trim(IIf(IsNull(RsTemp!PRODUCT_CODE), "", RsTemp!PRODUCT_CODE))	
        '            If CheckBOMForItem(mProductCode, pItemCode) = True Then	
        With SprdMain
            .Row = .ActiveRow

            .Col = ColItemCode
            .Text = Trim(pItemCode) '' Trim(IIf(IsNull(RsTemp!RM_CODE), "", RsTemp!RM_CODE))	

            If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                mItemDesc = MasterNo
            Else
                MsgInformation("Invalid Product Code")
                FillItemDescPart = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                mItemUOM = MasterNo
            End If

            .Col = ColItemDesc
            .Text = Trim(mItemDesc)

            .Col = ColUom
            .Text = Trim(mItemUOM)

            mRate = CStr(GetLatestItemCostFromMRR(pItemCode, mItemUOM, 1, (txtPMemoDate.Text), "L", "ST", ""))
            .Col = ColRate
            .Text = VB6.Format(mRate, "0.0")

            .Col = ColStockType
            .Text = IIf(Trim(.Text) = "", IIf(VB.Left(cboType.Text, 1) = "C", "CS", "ST"), Trim(.Text))
        End With
        FillItemDescPart = True
        Exit Function
        '            End If	
        '            RsTemp.MoveNext	
        '        Loop	
        '        MsgInformation "Item Not define in BOM of Product Code " & Trim(txtProductCode.Text)	
        '        FillItemDescPart = False	
        '        Exit Function	
        '    Else	
        '        MsgInformation "Item Not define in BOM of Product Code " & Trim(txtProductCode.Text)	
        '        FillItemDescPart = False	
        '        Exit Function	
        '    End If	
        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Function CheckBOMForItem(ByRef mProductCode As String, ByRef pItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckBOMForItem = True

        '    SqlStr = " SELECT * " & vbCrLf _	
        ''            & " FROM VW_PRD_BOM_TRN IH" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf _	
        ''            & " AND IH.RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' " & vbCrLf _	
        ''            & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"	
        '	
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	
        '    If RsTemp.EOF = False Then	
        '        CheckBOMForItem = True	
        '    Else	
        '        CheckBOMForItem = False	
        '    End If	
        Exit Function
ERR1:
        CheckBOMForItem = False
    End Function

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row
            .Col = 1
            txtPMemoNo.Text = .Text
            txtPMemoNo_Validating(txtPMemoNo, New System.ComponentModel.CancelEventArgs(False))
            If txtPMemoNo.Enabled = True Then txtPMemoNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenMemoNo() As String

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf _
            & " FROM PRD_REWORK_HDR  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    'mAutoGen = CDbl(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenMemoNo = mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double
        Dim mEntryDate As String
        Dim pErrorDesc As String
        'Dim RsTemp As ADODB.Recordset = Nothing	
        Dim mInCCCode As String
        Dim xStockRowNo As Integer
        Dim mRecdDate As String
        Dim mUpdateQty As Double
        Dim mSendDept As String
        Dim xAutoProductionIssue As Boolean
        Dim mProductSeqNo As Integer
        Dim mStockType As String
        Dim mProdCode As String
        Dim mDivisionCode As Double
        Dim mSupplierCode As String
        Dim mOrgBillNO As Double
        Dim mOrdBillDate As String
        Dim mItemRate As Double
        Dim mApproved As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "CCCODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mInCCCode = IIf(IsDBNull(MasterNo), "-1", MasterNo)
        Else
            mInCCCode = "-1"
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If


        mApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime


        txtRecdQty.Text = CStr(Val(txtReWorkQty.Text))

        If Val(txtRecdQty.Text) = 0 Then
            mRecdDate = ""
        Else
            mRecdDate = VB6.Format(IIf(txtRecdDate.Text = "", PubCurrDate, txtRecdDate.Text), "DD-MMM-YYYY")
        End If

        SqlStr = ""
        mPMemoNo = Val(txtPMemoNo.Text)
        If Val(txtPMemoNo.Text) = 0 Then
            mPMemoNo = CDbl(AutoGenMemoNo())
        End If
        txtPMemoNo.Text = CStr(mPMemoNo)
        If ADDMode = True Then
            lblMKey.Text = CStr(mPMemoNo)
            SqlStr = " INSERT INTO PRD_REWORK_HDR  " & vbCrLf _
                & " (COMPANY_CODE,FYEAR,AUTO_KEY_REF," & vbCrLf _
                & " REF_DATE, PREP_TIME, PROD_DATE, DEPT_CODE, SHIFT_CODE,PROD_TYPE," & vbCrLf _
                & " EMP_CODE, REMARKS, BOOKTYPE,  " & vbCrLf _
                & " SEND_DEPT_CODE, PRODUCT_CODE, REWORK_QTY," & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE,RECD_QTY,RECD_DATE,DIV_CODE, REWORK_COST, REWORK_MANDAYS, AUTO_KEY_MRR,MRR_DATE, MATERIAL_COST,APPROVED) " & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & "," & mPMemoNo & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & txtRefTM.Text & "','HH24:MI'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " '" & cboShiftcd.Text & "', " & vbCrLf _
                & " '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & VB.Left(lblBookType.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSendDept.Text) & "', '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & Val(txtReWorkQty.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & vbCrLf _
                & " " & Val(txtRecdQty.Text) & ",TO_DATE('" & VB6.Format(mRecdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & "," & vbCrLf _
                & " " & Val(txtReworkCost.Text) & "," & Val(txtReWorkManDays.Text) & "," & Val(txtMRRNo.Text) & ",TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(lblMaterialCost.Text) & ",'" & mApproved & "')"



        ElseIf MODIFYMode = True Then
            mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()
            SqlStr = " UPDATE PRD_REWORK_HDR  SET " & vbCrLf _
                & " AUTO_KEY_REF=" & mPMemoNo & ",  APPROVED='" & mApproved & "'," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PROD_DATE=TO_DATE('" & VB6.Format(txtProdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PREP_TIME=TO_DATE('" & txtRefTM.Text & "','HH24:MI')," & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " SHIFT_CODE='" & cboShiftcd.Text & "', " & vbCrLf _
                & " PROD_TYPE= '" & VB.Left(cboType.Text, 1) & "'," & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " SEND_DEPT_CODE='" & MainClass.AllowSingleQuote(txtSendDept.Text) & "', " & vbCrLf _
                & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
                & " REWORK_QTY=" & Val(txtReWorkQty.Text) & "," & vbCrLf _
                & " BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "', DIV_CODE=" & mDivisionCode & ", REWORK_COST=" & Val(txtReworkCost.Text) & ",REWORK_MANDAYS=" & Val(txtReWorkManDays.Text) & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'), RECD_QTY=" & Val(txtRecdQty.Text) & ",RECD_DATE=TO_DATE('" & VB6.Format(mRecdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ", MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),MATERIAL_COST=" & Val(lblMaterialCost.Text) & "" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_REF=" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(pErrorDesc, mInCCCode, xStockRowNo, mDivisionCode) = False Then GoTo ErrPart


        If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            mUpdateQty = Val(txtRecdQty.Text) ''Val(txtReWorkQty.Text)	
            xStockRowNo = xStockRowNo + 1
            If UpdateStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "WC", Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), CStr(-1), Val(CStr(mUpdateQty)), 0, "O", 0, 0, CStr(0), "", (txtDept.Text), (txtDept.Text), mInCCCode, "N", "To : " & txtSendDept.Text & "  : (CR Rework)", "-1", ConPH, mDivisionCode, VB.Left(cboType.Text, 1), "") = False Then GoTo ErrPart

            xStockRowNo = xStockRowNo + 1
            If UpdateStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "CR", Trim(txtProductCode.Text), Trim(lblProductionUOM.Text), CStr(-1), Val(CStr(mUpdateQty)), 0, "I", 0, 0, CStr(0), "", (txtSendDept.Text), (txtSendDept.Text), mInCCCode, "N", "From : " & txtDept.Text & "  : (CR Rework)", "-1", ConWH, mDivisionCode, VB.Left(cboType.Text, 1), "") = False Then GoTo ErrPart


            If GetCRData(CDbl(txtMRRNo.Text), Trim(txtProductCode.Text), mSupplierCode, mOrgBillNO, mOrdBillDate, mItemRate) = False Then GoTo ErrPart

            If UpdateCRTRN(PubDBCn, Val(txtPMemoNo.Text), (txtPMemoDate.Text), ConStockRefType_REWORK, mSupplierCode, (txtMRRNo.Text), (txtMRRDate.Text), CStr(Val(CStr(mOrgBillNO))), mOrdBillDate, Trim(txtProductCode.Text), CDbl(txtReWorkQty.Text), lblProductionUOM.Text, mItemRate, "WC", "O", (txtPMemoDate.Text), Val(CStr(mDivisionCode)), (txtDept.Text)) = False Then GoTo ErrPart

            If UpdateCRTRN(PubDBCn, Val(txtPMemoNo.Text), (txtPMemoDate.Text), ConStockRefType_REWORK, mSupplierCode, (txtMRRNo.Text), (txtMRRDate.Text), CStr(Val(CStr(mOrgBillNO))), mOrdBillDate, Trim(txtProductCode.Text), CDbl(txtReWorkQty.Text), lblProductionUOM.Text, mItemRate, "CR", "I", (txtPMemoDate.Text), Val(CStr(mDivisionCode)), (txtSendDept.Text)) = False Then GoTo ErrPart
        End If

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        txtPMemoNo.Text = CStr(mPMemoNo)
        Exit Function
ErrPart:

        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        If pErrorDesc <> "" Then
            MsgInformation(pErrorDesc)
        End If
        RsPMemoMain.Requery()
        RsPMemoDetail.Requery()
        If Trim(Err.Description) <> "" Then
            MsgBox(Err.Description)
        End If
        If ADDMode = True Then
            lblMKey.Text = ""
            txtPMemoNo.Text = ""
        End If
        '    Resume	
    End Function

    Private Function UpdateDetail1(ByRef pErrorDesc As String, ByRef mInCCCode As String, ByRef xStockRowNo As Integer, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mStockType As String
        Dim mProdQty As Double
        Dim mReworkQty As Double
        Dim mCRWokQty As Double
        Dim mCostPcs As Double
        'Dim xStockRowNo As Long	
        Dim xItemCost As Double

        Dim mWIPStock As Double
        Dim mWIPReworkStock As Double
        Dim mSqlStr As String
        Dim RsBOM As ADODB.Recordset
        Dim mProductSeqNo As Integer
        Dim mProductionDate As String
        'Dim mEntryDate As String	
        Dim mReason As String
        Dim mToolNo As String
        Dim mTotalOpr As Integer
        Dim mOprSeq As Integer
        Dim xOPStockType As String
        Dim pOPRCode As String
        Dim pOPRDesc As String
        Dim pSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOperatorCode As String
        Dim mStockID As String
        Dim mDeptCode As String
        Dim xAutoProductionIssue As Boolean
        Dim mRate As Double
        Dim mAmount As Double

        SqlStr = " DELETE FROM PRD_REWORK_DET " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(lblMKey.Text) & " "
        PubDBCn.Execute(SqlStr)

        If DeleteCRTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        If DeleteStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text)) = False Then GoTo UpdateDetail1Err
        xStockRowNo = 1

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColReWorkQty
                mReworkQty = Val(.Text)

                .Col = ColAmount
                .Text = VB6.Format(mRate * mReworkQty, "0.00")

                mAmount = CDbl(VB6.Format(mRate * mReworkQty, "0.00"))

                .Col = ColStockType
                mStockType = IIf(VB.Left(cboType.Text, 1) = "R" Or VB.Left(cboType.Text, 1) = "P", "WR", "CR") ''MainClass.AllowSingleQuote(.Text)	

                .Col = ColToolNo
                mToolNo = Trim(.Text)

                .Col = ColReason
                mReason = Trim(.Text)

                .Col = ColCostPcs
                mCostPcs = Val(.Text)

                .Col = ColOPRERCode
                mOperatorCode = Trim(.Text)

                .Col = ColOPR
                pOPRDesc = Trim(.Text)
                If Trim(pOPRDesc) = "" Then
                    pOPRCode = ""
                Else
                    SqlStr = OperationQuery(Trim(txtProductCode.Text), Trim(txtDept.Text), "", Trim(pOPRDesc), Trim(txtPMemoDate.Text), "TRN.OPR_CODE")

                    '                pSqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _	
                    ''                        & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _	
                    ''                        & " WHERE " & vbCrLf _	
                    ''                        & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                    ''                        & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _	
                    ''                        & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _	
                    ''                        & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _	
                    ''                        & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(pOPRDesc) & "'"	

                    MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        pOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    Else
                        pOPRCode = ""
                    End If
                End If

                SqlStr = ""

                If mItemCode <> "" And mReworkQty > 0 Then
                    SqlStr = " INSERT INTO PRD_REWORK_DET ( " & vbCrLf _
                        & " COMPANY_CODE,AUTO_KEY_REF,SERIAL_NO,ITEM_CODE,ITEM_DESC, " & vbCrLf _
                        & " ITEM_UOM,STOCK_TYPE, REWORK_QTY, COST_PCS, REASON, TOOL_NO, OPR_CODE, OPERATOR_CODE, ITEM_RATE, ITEM_AMOUNT) " & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMKey.Text) & ", " & i & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mItemDesc & "', " & vbCrLf & " '" & mUOM & "','" & mStockType & "', " & vbCrLf _
                        & " " & mReworkQty & ",  " & vbCrLf & " " & mCostPcs & ",'" & MainClass.AllowSingleQuote(mReason) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mToolNo) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pOPRCode) & "', '" & MainClass.AllowSingleQuote(mOperatorCode) & "'," & vbCrLf _
                        & " " & mRate & ", " & mAmount & ")"

                    PubDBCn.Execute(SqlStr)

                    If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If CheckAutoIssue(VB6.Format(txtProdDate.Text, "DD/MM/YYYY"), mItemCode) = False Then ''RsCompany!AUTO_ISSUE = "N"	
                            mStockID = "PH"
                            mDeptCode = txtDept.Text

                        Else
                            mStockID = "WH"
                            mDeptCode = "STR"
                        End If

                        '	
                        If Val(txtRecdQty.Text) > 0 Then
                            xStockRowNo = xStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "ST", mItemCode, mUOM, CStr(-1), mReworkQty, 0, "O", xItemCost, xItemCost, pOPRCode, "", mDeptCode, mDeptCode, mInCCCode, "N", "From : " & mDeptCode & "  : (CR Rework)", "-1", mStockID, mDivisionCode, VB.Left(cboType.Text, 1), Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err


                            xStockRowNo = xStockRowNo + 1
                            If UpdateStockTRN(PubDBCn, ConStockRefType_REWORK, (txtPMemoNo.Text), xStockRowNo, (txtPMemoDate.Text), (txtPMemoDate.Text), "RS", mItemCode, mUOM, CStr(-1), mReworkQty, 0, "I", xItemCost, xItemCost, pOPRCode, "", mDeptCode, mDeptCode, mInCCCode, "N", "From : " & mDeptCode & "  : (CR Rework)", "-1", mStockID, mDivisionCode, VB.Left(cboType.Text, 1), Trim(txtProductCode.Text)) = False Then GoTo UpdateDetail1Err

                        End If
                    End If
                    ''mStockType '12-04-2006	
                End If
NextRec:
            Next
        End With
        pErrorDesc = ""
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        '    Resume	
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mCheckLastEntryDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mProductCode As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mTotalProduction As Double
        Dim mItemCode As String
        Dim mUOM As String
        Dim mReworkQty As Double
        Dim mStockQty As Double
        Dim mDivision As Double

        FieldsVarification = True

        If txtPMemoDate.Text = "" Then
            MsgBox("txtPMemoDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        ElseIf FYChk((txtPMemoDate.Text)) = False Then
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        End If

        If ValidateBranchLocking((txtPMemoDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Memo No or modify an existing Memo No")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPMemoMain.EOF = True Then Exit Function

        If lblApproval.Text = "Y" And ADDMode = True Then
            MsgBox("Cann't be Add New Record in Approval Form.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If txtPMemoDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPMemoDate.Focus()
            Exit Function
        End If

        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Product Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtProductCode.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = Trim(MasterNo)
            Else
                MsgBox("Invaild Division Name is Blank", vbInformation)
                FieldsVarification = False
                If cboDivision.Enabled = True Then cboDivision.Focus()
                Exit Function
            End If
        End If

        If Trim(txtSendDept.Text) = "" Then
            MsgBox("Send Dept is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtSendDept.Focus()
            Exit Function
        End If
        If Val(txtReWorkQty.Text) <= 0 Then
            MsgBox("Rework Qty Cann't be Zero.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReWorkQty.Focus()
            Exit Function
        End If

        If Val(txtReworkCost.Text) <= 0 Then
            MsgBox("Rework Cost Cann't be Zero.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReworkCost.Focus()
            Exit Function
        End If

        If Val(txtReWorkManDays.Text) <= 0 Then
            MsgBox("Rework Man Days Cann't be Zero.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReworkCost.Focus()
            Exit Function
        End If

        If lblShow.Text = "R" Then
            If Val(txtRecdQty.Text) <= 0 Then
                MsgBox("Recd Qty Cann't be Zero.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtRecdQty.Enabled = True Then txtRecdQty.Focus()
                Exit Function
            End If

            If Val(txtRecdQty.Text) <> Val(txtReWorkQty.Text) Then
                MsgBox("Recd Qty must be Equal to Rework Qty.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtRecdQty.Enabled = True Then txtRecdQty.Focus()
                Exit Function
            End If
        End If

        '    If ADDMode = True Then	
        If CheckUnapprovedMRR = True Then
            FieldsVarification = False
            Exit Function
        End If
        '    End If	

        '    If Trim(txtDept.Text) <> Trim(txtSendDept.Text) Then	
        '        MsgBox "Send And Recd Dept Should be Same.", vbInformation	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	

        If PubSuperUser <> "S" Then
            If Val(txtRecdQty.Text) > Val(txtReWorkQty.Text) Then
                MsgBox("Recd Qty Cann't be Greater Than Rework Qty.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtRecdQty.Enabled = True Then txtRecdQty.Focus()
                Exit Function
            End If

            '        If Val(txtRecdQty.Text) > 0 And txtRecdQty.Enabled = False Then	
            '            MsgBox "You already Received. Cann't be modify.", vbInformation	
            '            FieldsVarification = False	
            '            Exit Function	
            '        End If	
        End If

        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(True))

        If Val(txtReWorkQty.Text) > Val(txtAvailableQty.Text) Then
            MsgBox("Rework Qty Cann't Greater than Available Qty.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtReWorkQty.Focus()
            Exit Function
        End If

        If Trim(cboShiftcd.Text) = "" Then
            MsgBox("Shift is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboShiftcd.Focus()
            Exit Function
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        If txtDept.Text = "" Then
            MsgBox("From Deptt is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If cboType.Text = "" Then
            MsgBox("Production Type is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboType.Enabled = True Then cboType.Focus()
            Exit Function
        End If

        If txtEmp.Text = "" Then
            MsgBox("Employee is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColReWorkQty
                mReworkQty = Val(.Text)

                '            If CheckItemReworkQty(cntRow) = False Then	
                '                FieldsVarification = False	
                '                Exit Function	
                '            End If	

                If CheckAutoIssue(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"), mItemCode) = False Then ''RsCompany!AUTO_ISSUE = "N"	
                    mStockQty = GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, (txtDept.Text), "ST", "", ConPH, mDivision, ConStockRefType_REWORK, Val(txtPMemoNo.Text))
                Else
                    mStockQty = GetBalanceStockQty(mItemCode, (txtPMemoDate.Text), mUOM, "STR", "ST", "", ConWH, mDivision, ConStockRefType_REWORK, Val(txtPMemoNo.Text))
                End If

                If mStockQty < mReworkQty Then
                    MsgBox("Stock Qty is not enough.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColReWorkQty)
                    Exit Function
                End If
            Next
        End With

        If lblShow.Text = "S" Then
            If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(lblDept.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        Else
            If ValidateDeptRight(PubUserID, Trim(txtSendDept.Text), UCase(Trim(lblSendDept.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        mCheckLastEntryDate = GetLastEntryDate()


        If PubSuperUser <> "S" Then
            If mCheckLastEntryDate <> "" Then
                mCheckLastEntryDate = DateAdd("d", -1, mCheckLastEntryDate)
                If CDate(txtPMemoDate.Text) < CDate(mCheckLastEntryDate) Then
                    MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        Call CalcTots()
        '     If PubSuperUser = "U" Then	
        '        If MainClass.ValidateWithMasterTable(PubUserEMPCode, "EMP_CODE", "EMP_DEPT_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
        '            mDeptCode = MasterNo	
        '            If UCase(Trim(txtDept.Text)) <> UCase(Trim(mDeptCode)) Then	
        '                MsgBox "You Are Not in This Dept.", vbInformation	
        '                FieldsVarification = False	
        '            End If	
        '        Else	
        '            MsgBox "Invalid Emp Code.", vbInformation	
        '            FieldsVarification = False	
        '        End If	
        '    End If	

        '    If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False: Exit Function	
        '    If MainClass.ValidDataInGrid(SprdMain, ColprodQty, "N", "Please Check Produce Quantity.") = False Then FieldsVarification = False: Exit Function	
        '    If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False: Exit Function	

        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function

    Private Function GetLastEntryDate() As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = "SELECT Max(REF_DATE) AS  REF_DATE " & vbCrLf _
            & " FROM PRD_REWORK_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND PROD_TYPE='" & VB.Left(cboType.Text, 1) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetLastEntryDate = IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value)
        End If

        Exit Function
ErrPart:

    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmCRRework_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        If lblApproval.Text = "N" Then
            Me.Text = "Customer Return Rework"
        Else
            Me.Text = "Customer Return Rework (Approval)"
        End If

        SqlStr = ""
        SqlStr = "Select * from PRD_REWORK_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_REWORK_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT  AUTO_KEY_REF MEMO_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf & " DEPT_CODE FROM_DEPT,SEND_DEPT_CODE,SHIFT_CODE,REWORK_QTY,RECD_QTY,DECODE(REWORK_QTY-RECD_QTY,0,'Complete','Pending') AS STATUS, DECODE(PROD_TYPE,'R','Rework','Customer Rej') AS Prod_Type,REMARKS " & vbCrLf & " FROM PRD_REWORK_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        SqlStr = SqlStr & vbCrLf & " ORDER BY REF_DATE,AUTO_KEY_REF"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1200)
            .Col = 1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .set_ColWidth(2, 1200)
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1000)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' OperationModeSingle	
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.25)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 6)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 23)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPMemoDetail.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 6)

            .Col = ColReWorkQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)


            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 4)
            .ColHidden = True

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)


            .Col = ColOPR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 8)

            .Col = ColOPRERCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("OPERATOR_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("REASON").DefinedSize
            .set_ColWidth(.Col, 12)

            .Col = ColToolNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsPMemoDetail.Fields("TOOL_NO").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColCostPcs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .set_ColWidth(.Col, 6)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCostPcs, ColCostPcs)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockType)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPMemoDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPMemoMain
            txtPMemoNo.Maxlength = .Fields("AUTO_KEY_REF").Precision
            txtPMemoDate.Maxlength = 10
            txtDept.Maxlength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize
            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize
            txtProdDate.Maxlength = 10
            txtRefTM.Maxlength = 5
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String
        Dim mEntryDate As String

        Dim mDivisionCode As Integer
        Dim mDivisionDesc As String
        Dim mAvailable As Double
        Dim mApproved As String

        With RsPMemoMain
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtPMemoNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtPMemoDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")
                txtProdDate.Text = VB6.Format(IIf(IsDbNull(.Fields("PROD_DATE").Value), "", .Fields("PROD_DATE").Value), "DD/MM/YYYY")

                txtRefTM.Text = VB6.Format(IIf(IsDbNull(.Fields("PREP_TIME").Value), "", .Fields("PREP_TIME").Value), "HH:MM")

                mEntryDate = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")
                mEntryDate = mEntryDate & vbCrLf & IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value) & " - " & VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY HH:MM")
                txtEntryDate.Text = mEntryDate

                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")
                lblMaterialCost.Text = VB6.Format(IIf(IsDbNull(.Fields("MATERIAL_COST").Value), 0, .Fields("MATERIAL_COST").Value), "0.00")

                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
                cboShiftcd.Text = IIf(IsDbNull(.Fields("SHIFT_CODE").Value), "", .Fields("SHIFT_CODE").Value)

                mProdType = IIf(IsDbNull(.Fields("PROD_TYPE").Value), "R", .Fields("PROD_TYPE").Value)
                cboType.SelectedIndex = 0


                mApproved = IIf(IsDbNull(.Fields("APPROVED").Value), "N", .Fields("APPROVED").Value)
                chkApproved.CheckState = IIf(mApproved = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkApproved.Enabled = IIf(mApproved = "Y", False, IIf(lblApproval.Text = "N", False, True))


                txtSendDept.Text = IIf(IsDbNull(.Fields("SEND_DEPT_CODE").Value), "", .Fields("SEND_DEPT_CODE").Value)
                txtSendDept_Validating(txtSendDept, New System.ComponentModel.CancelEventArgs(False))

                txtProductCode.Text = IIf(IsDbNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value)
                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    lblProductCode.text = MasterNo
                End If


                txtReWorkQty.Text = IIf(IsDbNull(.Fields("REWORK_QTY").Value), "", .Fields("REWORK_QTY").Value)

                txtReworkCost.Text = IIf(IsDbNull(.Fields("REWORK_COST").Value), "", .Fields("REWORK_COST").Value)
                txtReWorkManDays.Text = IIf(IsDbNull(.Fields("REWORK_MANDAYS").Value), "", .Fields("REWORK_MANDAYS").Value)

                txtEmp.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                txtRecdDate.Text = VB6.Format(IIf(IsDbNull(.Fields("RECD_DATE").Value), "", .Fields("RECD_DATE").Value), "DD/MM/YYYY")
                txtRecdQty.Text = IIf(IsDbNull(.Fields("RECD_QTY").Value), "", .Fields("RECD_QTY").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblProductionUOM.text = Trim(MasterNo)
                End If


                mAvailable = GetCRStockQty(CDbl(Trim(txtMRRNo.Text)), Trim(txtProductCode.Text), (txtDept.Text), mDivisionCode, "WC", IIf(Val(txtPMemoNo.Text) > 0, ConStockRefType_REWORK, "") & Val(txtPMemoNo.Text))
                mAvailable = mAvailable - GetUnApprovedQty(Trim(txtProductCode.Text), mDivisionCode)
                txtAvailableQty.Text = VB6.Format(mAvailable, "0.00")


                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False

                '            If Val(txtRecdQty.Text) <> 0 Then	
                '                fraRecdDetail.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)	
                '            End If	

                Call ShowDetail1(mDivisionCode)
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtPMemoNo.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowDetail1(ByRef mDivisionCode As Integer)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mOPRCode As String
        Dim mOPRDesc As String

        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_REWORK_DET  " & vbCrLf & " WHERE AUTO_KEY_REF = " & Val(lblMKey.Text) & " " & vbCrLf & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPMemoDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            i = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value))

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColRate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.00")

                SprdMain.Col = ColAmount
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value), "0.00")

                SprdMain.Col = ColReWorkQty
                SprdMain.Text = VB6.Format(Val(IIf(IsDbNull(.Fields("REWORK_QTY").Value), 0, .Fields("REWORK_QTY").Value)), "0.00")

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColCostPcs
                SprdMain.Text = VB6.Format(Val(IIf(IsDbNull(.Fields("COST_PCS").Value), "", .Fields("COST_PCS").Value)), "0.00")

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDbNull(.Fields("REASON").Value), "", .Fields("REASON").Value)

                SprdMain.Col = ColToolNo
                SprdMain.Text = IIf(IsDbNull(.Fields("TOOL_NO").Value), "", .Fields("TOOL_NO").Value)

                mOPRCode = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                If MainClass.ValidateWithMasterTable(mOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'") = True Then
                    mOPRDesc = MasterNo
                Else
                    mOPRDesc = ""
                End If
                SprdMain.Col = ColOPR
                SprdMain.Text = mOPRDesc

                SprdMain.Col = ColOPRERCode
                SprdMain.Text = IIf(IsDbNull(.Fields("OPERATOR_CODE").Value), "", .Fields("OPERATOR_CODE").Value)

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        Call CalcTots()
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh	
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtPMemoNo.Text = ""
        txtRefTM.Text = GetServerTime

        '    If CDate(txtRefTM.Text) < CDate("09:00") Then	
        '        txtPMemoDate.Text = Format(RunDate - 1, "DD/MM/YYYY")	
        '    Else	
        txtPMemoDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        txtProdDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
        '    End If	

        txtDept.Text = ""
        lblDept.Text = ""
        cboShiftcd.SelectedIndex = 0
        cboType.SelectedIndex = 0
        txtEmp.Text = ""
        lblEmp.Text = ""
        txtRemarks.Text = ""
        txtSendDept.Text = ""
        txtProductCode.Text = ""
        txtAvailableQty.Text = CStr(0)
        txtReWorkQty.Text = CStr(0)
        txtReworkCost.Text = CStr(0)
        txtReWorkManDays.Text = CStr(0)
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        lblMaterialCost.Text = "0.00"


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            chkApproved.CheckState = System.Windows.Forms.CheckState.Checked
            chkApproved.Enabled = False '' IIf(lblApproval.Text = "N", False, True)
        Else
            chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkApproved.Enabled = IIf(lblApproval.Text = "N", False, True)
        End If


        lblProductCode.Text = ""
        lblProductionUOM.Text = ""
        lblSendDept.Text = ""

        fraRecdDetail.Enabled = IIf(lblShow.Text = "R", True, False)
        FraRework.Enabled = IIf(lblShow.Text = "S", True, False)
        txtRecdQty.Text = CStr(0)
        txtRecdDate.Text = "" '' Format(PubCurrDate, "DD/MM/YYYY")	
        '    txtStockQty.Text = 0	

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        txtEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsPMemoMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtPMemoDate.Enabled = IIf(PubSuperUser = "S", True, mMode)
        txtDept.Enabled = mMode
        CmdSearchDept.Enabled = mMode

    End Sub
    Private Sub FrmCRRework_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmCRRework_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmCRRework_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	


        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(10935)
        Call FillCbo()
        AdoDCMain.Visible = False
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent

        Dim mCol As Short
        Dim mRow As Short
        Dim mPrevRow As Short
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mProdQty As Double
        Dim mReworkQty As Double


        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        With SprdMain
            mPrevRow = mRow - 1
            .Row = mPrevRow
            .Col = ColOPR
            If eventArgs.KeyCode = System.Windows.Forms.Keys.F5 And mRow > 1 And Trim(.Text) <> "" Then

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemDesc = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColReWorkQty
                mProdQty = Val(.Text)

                .Row = mRow
                .Col = ColItemCode
                .Text = mItemCode

                .Col = ColItemDesc
                .Text = mItemDesc

                .Col = ColUom
                .Text = mUOM

                .Col = ColReWorkQty
                .Text = CStr(mProdQty)

                SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, mRow, ColItemCode, mRow, False))
                MainClass.SetFocusToCell(SprdMain, mRow, ColOPR)
            End If
        End With
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain	
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
        '    End With	

    End Sub

    Private Sub txtAvailableQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAvailableQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAvailableQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAvailableQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEntryDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEntryDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub
    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Public Sub TxtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDivisionCode As Integer
        Dim mAvailable As Double

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If Trim(cboDivision.Text) = "" Then GoTo EventExitSub
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        txtAvailableQty.Text = "0.00"
        lblProductionUOM.Text = ""

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If



        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mMRRNo = Trim(txtMRRNo.Text)

        SqlStr = "SELECT AUTO_KEY_MRR, ITEM_CODE, ITEM_UOM, MRR_DATE, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) As ITEM_QTY" & vbCrLf _
            & " FROM DSP_CR_TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND AUTO_KEY_MRR=" & mMRRNo & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STOCK_TYPE='WC'" & vbCrLf _
            & " GROUP BY AUTO_KEY_MRR, MRR_DATE, ITEM_CODE,ITEM_UOM " & vbCrLf _
            & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtMRRDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value), "DD/MM/YYYY")
            mAvailable = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
            mAvailable = mAvailable - GetUnApprovedQty(Trim(txtProductCode.Text), mDivisionCode)
            txtAvailableQty.Text = mAvailable

            If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblProductCode.Text = Trim(MasterNo)
            End If

            lblProductionUOM.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdMRRSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMRRSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double


        If Trim(cboDivision.Text) = "" Then MsgInformation("Please select the Division Code") : Exit Sub
        If Trim(txtDept.Text) = "" Then MsgInformation("Please select the Dept Code") : Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = "SELECT AUTO_KEY_MRR, ITEM_CODE, MRR_DATE, SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY) - " & vbCrLf _
            & "NVL((SELECT SUM(REWORK_QTY) FROM PRD_REWORK_HDR WHERE COMPANY_CODE=TRN.COMPANY_CODE AND AUTO_KEY_MRR=TRN.AUTO_KEY_MRR" & vbCrLf _
            & " AND DEPT_CODE=TRN.DEPT_CODE AND DIV_CODE=TRN.DIV_CODE AND PRODUCT_CODE=TRN.ITEM_CODE AND APPROVED='N' AND PROD_TYPE='C' AND AUTO_KEY_REF<>" & Val(txtPMemoNo.Text) & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')),0)" & vbCrLf _
            & " As ITEM_QTY" & vbCrLf _
            & " FROM DSP_CR_TRN TRN" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DEPT_CODE='" & txtDept.Text & "'" & vbCrLf _
            & " AND DIV_CODE=" & mDivisionCode & " AND STOCK_TYPE='WC'" & vbCrLf _
            & " GROUP BY TRN.COMPANY_CODE, TRN.DEPT_CODE, TRN.DIV_CODE, AUTO_KEY_MRR, MRR_DATE, ITEM_CODE " & vbCrLf _
            & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1) * ITEM_QTY)>0"

        If MainClass.SearchGridMasterBySQL2(txtMRRNo.Text, SqlStr) = True Then
            txtMRRNo.Text = AcName
            txtProductCode.Text = AcName1
            'TxtMRRNo_Validate(False)
            TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtMRRDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPMemoDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPMemoDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtPMemoDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPMemoDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If

        If Trim(cboShiftcd.Text) = "C" Then
            txtProdDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY"))))
            txtProdDate.Text = VB6.Format(txtProdDate.Text, "DD/MM/YYYY")
        Else
            txtProdDate.Text = VB6.Format(txtPMemoDate.Text, "DD/MM/YYYY")
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblDept.text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmp.text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPMemoNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPMemoNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMemoNo.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtPMemoNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPMemoNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPMemoNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPMemoNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPMemoNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Public Sub txtPMemoNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMemoNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mPMemoNo As Double

        If Trim(txtPMemoNo.Text) = "" Then GoTo EventExitSub

        If Len(txtPMemoNo.Text) < 6 Then
            txtPMemoNo.Text = Val(txtPMemoNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsPMemoMain.EOF = False Then mPMemoNo = RsPMemoMain.Fields("AUTO_KEY_REF").Value

        SqlStr = "Select * From PRD_REWORK_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtPMemoNo.Text) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPMemoMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such P.Memo.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PRD_REWORK_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(CStr(mPMemoNo)) & "" & vbCrLf & " AND BOOKTYPE='" & VB.Left(lblBookType.Text, 1) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPMemoMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProdDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchProductCode_Click(cmdSearchProductCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mAvailable As Double
        Dim mItemUOM As String

        Dim mDivisionCode As Integer


        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblProductCode.text = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            Cancel = True
        End If

        If cboDivision.Text = "" Then
            '        If cboDivision.Enabled = True Then cboDivision.SetFocus	
            '        MsgInformation "Please Select Division."	
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblProductionUOM.text = MasterNo
            mItemUOM = MasterNo
        End If

        mAvailable = GetCRStockQty(CDbl(Trim(txtMRRNo.Text)), Trim(txtProductCode.Text), (txtDept.Text), mDivisionCode, "WC", IIf(Val(txtPMemoNo.Text) > 0, ConStockRefType_REWORK, "") & Val(txtPMemoNo.Text))
        mAvailable = mAvailable - GetUnApprovedQty(Trim(txtProductCode.Text), mDivisionCode)
        txtAvailableQty.Text = VB6.Format(mAvailable, "0.00")
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRecdDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecdQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecdQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRecdQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecdQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRefTM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefTM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReworkCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReworkCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReworkCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReworkCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReWorkManDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReWorkManDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReWorkManDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReWorkManDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReWorkQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReWorkQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReWorkQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReWorkQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSendDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSendDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSendDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSendDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSendDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSendDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSendDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchSendDept_Click(CmdSearchSendDept, New System.EventArgs())
    End Sub

    Private Sub txtSendDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSendDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtSendDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtSendDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE IN ('STR','PAD')") = True Then
            lblSendDept.text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code, Please select Only STR or PAD Dept.")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub CmdSearchSendDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchSendDept.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtSendDept.Text = AcName1
            lblSendDept.text = AcName
            If txtSendDept.Enabled = True Then txtSendDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Function GetUnApprovedQty(ByVal pItemCode As String, ByVal pDivision As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        SqlStr = ""
        SqlStr = "SELECT SUM(REWORK_QTY) AS BALQTY" & vbCrLf _
            & " FROM PRD_REWORK_HDR IH " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4) = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If pDivision <> -1 Then
            SqlStr = SqlStr & vbCrLf & "AND IH.DIV_CODE=" & pDivision & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.APPROVED='N' AND PROD_TYPE='C'"

        SqlStr = SqlStr & vbCrLf & "AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        If Val(txtPMemoNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND  IH.AUTO_KEY_REF<>" & Val(txtPMemoNo.Text) & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.REF_DATE<=TO_DATE('" & VB6.Format(txtPMemoDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBalStock.EOF = False Then
            If IsDBNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If
        RsBalStock = Nothing

        GetUnApprovedQty = mBalQty
        Exit Function
ErrPart:
        GetUnApprovedQty = 0
    End Function
End Class
