Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl

Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration
Imports AxFPSpreadADO

Friend Class frmSalesDSRequisition
    Inherits System.Windows.Forms.Form
    Dim RsDSSMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsDSSDetail As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim mAmendSchd As Boolean
    Dim FileDBCn As ADODB.Connection
    Dim mSearchStartRow As Integer

    Private Const ConRowHeight As Short = 14

    Dim pTempSeq As String

    Private Const ColItemCode As Short = 1
    Private Const ColCustPartNo As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColItemUOM As Short = 4
    Private Const ColStoreLoc As Short = 5
    Private Const ColItemDetail As Short = 6
    Private Const ColPreviousQty As Short = 7
    Private Const ColWeek1Qty As Short = 8
    Private Const ColWeek2Qty As Short = 9
    Private Const ColWeek3Qty As Short = 10
    Private Const ColWeek4Qty As Short = 11
    Private Const ColWeek5Qty As Short = 12
    Private Const ColSchdQnty As Short = 13
    Private Const ColAmendReason As Short = 14
    Private Const ColAmendNo As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub chkApprovalBH_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApprovalBH.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkApprovalPH_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApprovalPH.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Call DelTemp_DailyDetail()
            Clear1()
            SprdMain.Enabled = True
            txtDSNo.Enabled = False
            cmdPopulate.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsDSSMain.EOF = False Then RsDSSMain.MoveFirst()
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
        Dim mPOType As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer

        If ADDMode = True Then
            Exit Sub
        End If
        '    If CDate(PubCurrDate) > CDate(txtScheduleDate.Text) Then
        '        MsgInformation "MOnth Closed so Cann't be Modified."
        '        Exit Sub
        '    End If

        If chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked And chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked Then
            chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Unchecked
        ElseIf chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked Or chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("You Cann't be Amend Delivery Schedule.")
            Exit Sub
        ElseIf chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Unchecked And chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("You Cann't be Amend Delivery Schedule.")
            Exit Sub
        End If


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' AND SO_STATUS='O' AND SO_APPROVED='Y'"

        If RsCompany.Fields("COMPANY_CODE").Value = 32 Or RsCompany.Fields("COMPANY_CODE").Value = 16 Then

        Else
            If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
                mPOType = MasterNo
                If mPOType = "C" And PubSuperUser = "U" Then
                    MsgInformation("You Cann't be Amend Closed PO Delivery Schedule.")
                    Exit Sub
                End If
            Else
                MsgInformation("Invalid PO for such Supplier.")
                Exit Sub
            End If
        End If

        ADDMode = False
        MODIFYMode = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtDSNo.Enabled = False
        txtDSAmendNo.Text = CStr(Val(txtDSAmendNo.Text) + 1)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtScheduleDate.Enabled = False
        '    cmdAmendSchd.Enabled = False
        txtPONo.Enabled = False
        txtPODate.Enabled = False
        cmdPoSearch.Enabled = False

        SqlStr = " SELECT AUTO_KEY_SO, SO_DATE,CUST_PO_NO, CUST_PO_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_FROM " & vbCrLf & " FROM DSP_SALEORDER_HDR" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf & " AND AUTO_KEY_SO=" & Val(txtOurSONo.Text) & " " & vbCrLf & " AND SO_STATUS='O' AND SO_APPROVED='Y'"

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPOAmendNo.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value)
            lblAutoSoNo.Text = Val(txtOurSONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000")
            txtPOAmendDate.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_DATE").Value), "", RsTemp.Fields("AMEND_DATE").Value)
            txtWEF.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_WEF_FROM").Value), "", RsTemp.Fields("AMEND_WEF_FROM").Value)

            txtPODate.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value)
            txtPONo.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
            lblAutoSodate.Text = IIf(IsDbNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
        End If

        mAmendSchd = True
        Call cmdRefresh_Click(cmdRefresh, New System.EventArgs())

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmendReason
                .Text = ""
            Next
        End With
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtScheduleDate.Text)) = True Then
            Exit Sub
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSO_DS), txtScheduleDate.Text) = True Then
            Exit Sub
        End If
        If ValidateAccountLocking(PubDBCn, (txtScheduleDate.Text), (txtSupplierName.Text)) = True Then
            Exit Sub
        End If

        If Val(txtDSAmendNo.Text) > 0 Then
            MsgInformation("Amend DS Cann't be Deleted")
            Exit Sub
        End If

        If chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked Or chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Approved DS Cann't be Deleted")
            Exit Sub
        End If

        If txtDSNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsDSSMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_DELV_SCHLD_REQ_HDR", (txtDSNo.Text), RsDSSMain, "", "D") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_DELV_SCHLD_REQ_HDR", "AUTO_KEY_DELV", (lblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_DELV_SCHLD_REQ_DET WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_DELV_SCHLD_REQ_HDR WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsDSSMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsDSSMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdeMail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeMail.Click
        Dim mClosedOrder As String = ""
        Dim mEMailID As String = ""

        Exit Sub

        If MainClass.ValidateWithMasterTable(Val(lblAutoSoNo.Text), "MKEY", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            mClosedOrder = MasterNo
        Else
            MsgInformation("InValid PO No.")
            Exit Sub
        End If

        If mClosedOrder <> "C" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "SUPP_CUST_MAILID", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEMailID = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        If Trim(mEMailID) = "" Or Len(Trim(mEMailID)) < 5 Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ConfirmationLettereMail(mEMailID)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ConfirmationLettereMail(ByRef mEMailID As String)


        On Error GoTo ERR1
        'Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String


        If InsertIntoTempTable() = False Then GoTo ERR1

        SqlStr = " SELECT * FROM TEMP_DS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY ITEM_CODE, SERIAL_DATE"

        'Insert Data from Grid to PrintDummyData Table...

        mTitle = "ORDER CUM DELIVERY CONFIRMATION"
        mSubTitle = ""
        mRptFileName = "OrdercumDel.Rpt"

        Call ShoweMailReport(SqlStr, mRptFileName, mTitle, mSubTitle, mEMailID)

        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'Resume
    End Sub

    Private Sub ShoweMailReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mTitle As String, ByRef mSubTitle As String, ByRef empMailId As String)

        '        On Error GoTo ErrPart
        '        Dim crapp As New CRAXDRT.Application
        '        Dim RsTemp As New ADODB.Recordset
        '        Dim RS As New ADODB.Recordset

        '        Dim objRpt As CRAXDRT.Report
        '        Dim fPath As String


        '        Dim SqlStr As String = ""


        '        mRPTName = PubReportFolderPath & mRPTName
        '        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)


        '        objRpt = crapp.OpenReport(mRPTName)

        '        With objRpt
        '            Call ClearCRpt8Formulas(objRpt)
        '            .DiscardSavedData()
        '            '        Report1.Connect = STRRptConn
        '            .Database.SetDataSource(RS)
        '            SetCrpteMail(objRpt, 1, mTitle, mSubTitle)
        '            .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint
        '        End With

        '        fPath = mLocalPath & "\ODC" & Val(txtDSNo.Text) & ".pdf"

        '        With objRpt
        '            .ExportOptions.FormatType = CRAXDDRT.CRExportFormatType.crEFTPortableDocFormat
        '            .ExportOptions.DestinationType = CRAXDDRT.CRExportDestinationType.crEDTDiskFile
        '            .ExportOptions.DiskFileName = fPath
        '            '    .ExportOptions.PDFExportAllPages = True
        '            .Export(False)
        '        End With

        '        objRpt = Nothing

        '        If empMailId = "" Or fPath = "" Then
        '            MsgInformation("Please Enter the Vaild eMail ID.")
        '            Exit Sub
        '        Else
        '            If SendeMail(fPath, empMailId) = False Then GoTo ErrPart
        '            MsgInformation("Message sent successfully.")
        '        End If

        '        Exit Sub
        'ErrPart:
        '        'Resume
        '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Connect_Report_To_Database(ByRef Report1 As CRAXDRT.Report, ByRef mRs As ADODB.Recordset)
        On Error GoTo ErrPart
        Dim I As Short

        'Dim tables As CRAXDRT.DatabaseTables
        'Dim csprop As CRAXDRT.ConnectionProperties
        'Dim cs As CRAXDRT.ConnectionProperty
        'Dim tablecount As Integer
        'Dim CRXDatabase As CRAXDRT.Database
        '
        ''Dim crtable As CRAXDRT.DatabaseTable
        '
        '
        ''  Report1.Database.Tables.Item(1).SetLogOnInfo "HEMA", "SERVER", "HEMAERP", "JUN2011"
        '  Report1.Database.Tables.Item(1).SetDataSource RS, 3
        ''CRXDatabase.SetDataSource mRS, 3, 1
        ''CRXDatabase.LogOnServer "crdb_odbc.dll", "SERVER", "SERVER", "HEMAERP", "JUN2011"
        ''
        ''Exit Sub
        '
        'Set tables = Report1.Database.tables
        '
        '
        'tablecount = tables.Count
        '
        'For I = 1 To tablecount
        ''    MsgBox tables.Item(I).Name
        '    Set csprop = tables.Item(tablecount).ConnectionProperties
        '    csprop.Item("Data Source") = DBConSERVICENAME        '' "MYERP"
        ''    csprop.Item("SERVICE NAME") = "MYERP"
        '    csprop.Item("User ID") = DBConUID           ''"TAXATION"
        '    csprop.Item("Password") = DBConPWD          ''"TAX"
        'Next
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Function SetCrpteMail(ByRef Report2 As CRAXDRT.Report, ByRef mNoOfCopies As Short, ByRef mTitle As String, Optional ByRef mSubTitle As String = "", Optional ByRef mDocTitle As Boolean = False, Optional ByRef xMenuID As String = "") As Boolean
        '        On Error GoTo ERR1
        '        Dim ICodeWidth As String
        '        Dim CompanyName_Renamed As String
        '        Dim BranchName As String
        '        Dim CompanyAdd As Object
        '        Dim mCompanyAddress As String
        '        Dim UserID, CompanyPhone, RunDate As Object
        '        Dim PageNo As String
        '        Dim xDocNo As String
        '        Dim xOrigDate As String
        '        Dim xRevNo As String
        '        Dim xRevDate As String


        '        If RsCompany.Fields("PrintTopCompanyName").Value = "Y" Then
        '            ''CompanyName = IIf(RsCompany.Fields("PrintCompanyFull_ShortName").Value = "F", RsCompany.Fields("Company_Name").Value, IIf(IsNull(RsCompany.Fields("CompanyShortName").Value), "", RsCompany.Fields("CompanyShortName").Value))
        '            CompanyName_Renamed = RsCompany.Fields("Company_Name").Value
        '        Else
        '            CompanyName_Renamed = ""
        '        End If

        '        ''BranchName = RsCompany.Fields("BranchName").Value

        '        If RsCompany.Fields("PrintTopCompanyAddress").Value = "Y" Then
        '            CompanyAdd = "" & RsCompany.Fields("COMPANY_ADDR").Value & ",  " & RsCompany.Fields("COMPANY_CITY").Value & " , " & RsCompany.Fields("COMPANY_STATE").Value & " - " & RsCompany.Fields("COMPANY_PIN").Value & ""
        '        Else
        '            CompanyAdd = ""
        '        End If
        '        If RsCompany.Fields("PRintTopCompanyPhone").Value = "Y" Then
        '            CompanyPhone = "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value & " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value & " e-Mail : " & RsCompany.Fields("COMPANY_MAILID").Value
        '        Else
        '            CompanyPhone = ""
        '        End If
        '        If RsCompany.Fields("PrintTopCompanyAddress").Value = "N" Then
        '            mCompanyAddress = ""
        '        End If


        '        Report2.DiscardSavedData()
        '        '    MainClass.ReportWindow Report2, mTitle
        '        '    Report2.FormulaFields.GetItemByName("CompanyName").Text = "" & CompanyName & ""
        '        AssignCRpt8Formulas(Report2, "CompanyName", "'" & CompanyName_Renamed & "'")
        '        AssignCRpt8Formulas(Report2, "CompanyAddress", "'" & CompanyAdd & "'")
        '        AssignCRpt8Formulas(Report2, "Title", "'" & UCase(mTitle) & "'")
        '        AssignCRpt8Formulas(Report2, "SubTitle", "'" & mSubTitle & "'")



        '        If RsCompany.Fields("PrintBotCompanyName").Value = "Y" Then
        '            CompanyName_Renamed = RsCompany.Fields("Company_Name").Value
        '        Else
        '            CompanyName_Renamed = ""
        '        End If
        '        CompanyAdd = IIf(RsCompany.Fields("PrintBotCompanyAddress").Value = "Y", "" & RsCompany.Fields("COMPANY_ADDR").Value & " ,    " & RsCompany.Fields("COMPANY_CITY").Value & ",    " & RsCompany.Fields("COMPANY_STATE").Value & " -   " & RsCompany.Fields("COMPANY_PIN").Value & "", "")
        '        CompanyPhone = IIf(RsCompany.Fields("PrintBotCompanyPhone").Value = "Y", "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value & " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value & " e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value, "")

        '        AssignCRpt8Formulas(Report2, "CompanyBotLine1", "'" & CompanyAdd & "'")
        '        AssignCRpt8Formulas(Report2, "CompanyBotLine2", "'" & IIf(IsDbNull(CompanyPhone), "", CompanyPhone) & "'")

        '        If RsCompany.Fields("Printuser").Value = "Y" Then
        '            UserID = PubUserID
        '        Else
        '            UserID = ""
        '        End If
        '        If RsCompany.Fields("PrintrunDate").Value = "Y" Then
        '            RunDate = Str(Today.ToOADate)
        '        Else
        '            RunDate = " "
        '        End If
        '        If RsCompany.Fields("PrintPageNo").Value = "Y" Then
        '            PageNo = "Y"
        '        Else
        '            PageNo = "N"
        '        End If

        '        If mDocTitle = True Then
        '            If Trim(xMenuID) <> "" Then
        '                If MainClass.SetReportDocDetail(xMenuID, PubDBCn, xDocNo, xOrigDate, xRevNo, xRevDate) = True Then
        '                    AssignCRpt8Formulas(Report2, "DocNo", "'" & xDocNo & "'")
        '                    AssignCRpt8Formulas(Report2, "OrigDate", "'" & xOrigDate & "'")
        '                    AssignCRpt8Formulas(Report2, "RevNo", "'" & xRevNo & "'")
        '                    AssignCRpt8Formulas(Report2, "RevDate", "'" & xRevDate & "'")
        '                End If
        '            End If
        '        End If

        '        AssignCRpt8Formulas(Report2, "UserID", "'" & UserID & "'")
        '        AssignCRpt8Formulas(Report2, "PrintDate", "'" & RunDate & "'")
        '        AssignCRpt8Formulas(Report2, "PrintPageNo", "'" & PageNo & "'")

        '        Report2.TopMargin = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINTOP").Value), 0, RsCompany.Fields("REPORTMARGINTOP").Value) * 1440
        '        Report2.BottomMargin = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINBOT").Value), 0, RsCompany.Fields("REPORTMARGINBOT").Value) * 1440
        '        Report2.LeftMargin = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINLEFT").Value), 0, RsCompany.Fields("REPORTMARGINLEFT").Value) * 1440
        '        Report2.RightMargin = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINRIGHT").Value), 0, RsCompany.Fields("REPORTMARGINRIGHT").Value) * 1440

        '        '    Report2.Connect = STRRptConn
        '        SetCrpteMail = True
        '        Exit Function
        'ERR1:
        '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        ''Resume
    End Function


    Private Function SendeMail(ByRef mAttachmentFile As String, ByRef mTo As String) As Boolean
        On Error GoTo ErrPart

        Dim mCC As String
        Dim mFrom As String
        Dim mSubject As String


        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String

        SendeMail = False

        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************


        mFrom = GetEMailID("DSP_MAIL_TO")
        mCC = GetEMailID("DSP_MAIL_TO")

        mSubject = "Order Cum Delivery Confirmation"


        mBodyText = "<html><body><br />" & "<b></b>" & mSubject & "<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

        If Trim(mTo) <> "" Then
            If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
        End If

        SendeMail = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        SendeMail = False
        '    Resume
    End Function
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        '    If chkStatus.Value = vbChecked Then
        '        MsgInformation "Posted PO Cann't be Modified"
        '        Exit Sub
        '    End If
        '

        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked Or chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked Then
                Exit Sub
            End If
            '        Exit Sub  ''26/12/2015
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub CmdPopFromFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPopFromFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        Dim pIsODSaleOrder As Boolean

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls||*.xlsx", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        If Trim(strFilePath) = "" Then
            GoTo NormalExit
        End If

        If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            pIsODSaleOrder = IIf(Trim(MasterNo) = "Y", True, False)
        Else
            pIsODSaleOrder = False
            Exit Sub
        End If

        If pIsODSaleOrder = False Then
            'Clear1()

            pTempSeq = MainClass.AutoGenRowNo("DSP_DAILY_SCHLD_DET", "RowNo", PubDBCn)

            Call DelTemp_DailyDetail()

            MainClass.ClearGrid(SprdMain, ConRowHeight)

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                Call PopulateFromXLSFile(strFilePath)
            Else
                MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

                Call PopulateFromXLSFile_JMD(strFilePath)
            End If

            FormatSprdMain(-1)
        Else
            '    Call DelTemp_DailyDetail
            pTempSeq = MainClass.AutoGenRowNo("DSP_DAILY_SCHLD_DET", "RowNo", PubDBCn)

            Call DelTemp_DailyDetail()

            'MainClass.ClearGrid(SprdMain, ConRowHeight)
            'FormatSprdMain(-1)

            MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)


            Call PopulateODFromXLSFile(strFilePath)
        End If

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    '    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

    '        On Error GoTo ErrPart
    '        Dim cntRow As Integer
    '        Dim SqlStr As String = ""
    '        Dim mItemCode As String
    '        Dim mCheckItemCode As String

    '        Dim mItemDesc As String
    '        Dim mUOM As String
    '        Dim mDailyQty() As Double



    '        'Dim mStockType As String
    '        'Dim mStockQty As Double
    '        'Dim mAdjQty As Double
    '        'Dim xSqlStr As String
    '        Dim mSqlStr As String
    '        'Dim RsTemp As ADODB.Recordset = Nothing
    '        Dim RsFile As ADODB.Recordset
    '        Dim FileConnStr As String

    '        Dim strTemp As String
    '        Dim strWkShName As String
    '        Dim strError As String = ""
    '        Dim mFieldNo As Integer
    '        Dim I As Integer
    '        Dim mTotalQty As Double

    '        Dim mSerialDate As String
    '        Dim mWeek1Qty As Double
    '        Dim mWeek2Qty As Double
    '        Dim mWeek3Qty As Double
    '        Dim mWeek4Qty As Double
    '        Dim mWeek5Qty As Double

    '        ReDim mDailyQty(30)

    '        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
    '        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
    '        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
    '        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

    '        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
    '            GoTo ErrPart
    '        End If

    '        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
    '        strWkShName = RsFile.Fields("Table_Name").Value

    '        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
    '        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

    '        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then
    '            If RsFile.EOF = False Then
    '                Do While Not RsFile.EOF
    '                    mCheckItemCode = Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
    '                    mTotalQty = 0
    '                    mWeek1Qty = 0
    '                    mWeek2Qty = 0
    '                    mWeek3Qty = 0
    '                    mWeek4Qty = 0
    '                    mWeek5Qty = 0

    '                    I = 0
    '                    For mFieldNo = 4 To 34
    '                        mDailyQty(I) = Val(IIf(IsDbNull(RsFile.Fields(mFieldNo).Value), 0, RsFile.Fields(mFieldNo).Value))
    '                        mTotalQty = mTotalQty + mDailyQty(I)

    '                        If I < 7 Then
    '                            mWeek1Qty = mWeek1Qty + mDailyQty(I)
    '                        ElseIf I < 14 Then
    '                            mWeek2Qty = mWeek2Qty + mDailyQty(I)
    '                        ElseIf I < 21 Then
    '                            mWeek3Qty = mWeek3Qty + mDailyQty(I)
    '                        ElseIf I < 28 Then
    '                            mWeek4Qty = mWeek4Qty + mDailyQty(I)
    '                        Else
    '                            mWeek5Qty = mWeek5Qty + mDailyQty(I)
    '                        End If

    '                        I = I + 1
    '                    Next


    '                    If mCheckItemCode <> "" Then
    '                        With SprdMain
    '                            For cntRow = 1 To .MaxRows
    '                                .Row = cntRow
    '                                .Col = ColItemCode
    '                                mItemCode = Trim(.Text)

    '                                I = 0
    '                                If mItemCode = mCheckItemCode Then
    '                                    .Row = cntRow
    '                                    .Col = ColWeek1Qty
    '                                    .Text = VB6.Format(mWeek1Qty, "0.000")

    '                                    .Col = ColWeek2Qty
    '                                    .Text = VB6.Format(mWeek2Qty, "0.000")

    '                                    .Col = ColWeek3Qty
    '                                    .Text = VB6.Format(mWeek3Qty, "0.000")


    '                                    .Col = ColWeek4Qty
    '                                    .Text = VB6.Format(mWeek4Qty, "0.000")

    '                                    .Col = ColWeek5Qty
    '                                    .Text = VB6.Format(mWeek5Qty, "0.000")

    '                                    .Col = ColSchdQnty
    '                                    .Text = VB6.Format(mTotalQty, "0.000")

    '                                    SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf & " AND AUTO_KEY_DELV =" & Val(txtDSNo.Text) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "')"

    '                                    PubDBCn.Execute(SqlStr)

    '                                    For I = 0 To 30
    '                                        If I + 1 <= MainClass.LastDay(Month(CDate(txtScheduleDate.Text)), Year(CDate(txtScheduleDate.Text))) Then
    '                                            mSerialDate = VB6.Format(VB6.Format(I + 1, "00") & "/" & VB6.Format(txtScheduleDate.Text, "MM/YYYY"), "DD/MM/YYYY")
    '                                        Else
    '                                            mSerialDate = ""
    '                                        End If

    '                                        If mSerialDate <> "" Then
    '                                            SqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET " & " (USERID, TEMP_AUTO_KEY, AUTO_KEY_DELV, " & vbCrLf & " ITEM_CODE, SERIAL_DATE, PLANNED_QTY, " & vbCrLf & " ACTUAL_QTY, DELV_CNT, SUPP_CUST_CODE, " & vbCrLf & " SCHLD_DATE, REQ_DATE ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf & " " & Val(txtDSNo.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "'), " & vbCrLf & " " & mDailyQty(I) & ", 0, 0," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "'),TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "')) "
    '                                            PubDBCn.Execute(SqlStr)
    '                                        End If

    '                                    Next
    '                                    GoTo NextRecord
    '                                End If
    '                            Next
    '                        End With
    '                    End If
    'NextRecord:
    '                    RsFile.MoveNext()
    '                Loop
    '            End If
    '        End If

    '        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
    '        RsFile = Nothing

    '        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
    '            FileDBCn.Close()
    '            FileDBCn = Nothing
    '        End If

    '        '    CmdPopFromFile.Enabled = False
    '        Exit Sub
    'ErrPart:
    '        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    '        '        Resume
    '    End Sub
    Private Function GetSearchSOItems(ByRef mByCode As String, Optional ByRef mItemCode As String = "") As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT ID.ITEM_CODE,INVMST.ITEM_SHORT_DESC, ID.PART_NO "
        ElseIf mByCode = "S" Then
            mSqlStr = "SELECT ID.CUST_STORE_LOC, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, ID.PART_NO "
        Else
            mSqlStr = "SELECT INVMST.ITEM_SHORT_DESC,ID.ITEM_CODE, ID.PART_NO "
        End If

        mSqlStr = mSqlStr & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & txtOurSONo.Text & "" & vbCrLf _
            & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' "

        If mItemCode <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        End If

        GetSearchSOItems = mSqlStr
        Exit Function
ErrPart:
        GetSearchSOItems = ""

    End Function
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mCheckItemCode As String
        Dim mStoreLoc As String = ""
        Dim mStoreSOLoc As String = ""
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mDailyQty() As Double
        Dim RsTemp As ADODB.Recordset


        'Dim mStockType As String
        'Dim mStockQty As Double
        'Dim mAdjQty As Double
        'Dim xSqlStr As String
        Dim mSqlStr As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mFieldNo As Integer
        Dim I As Integer
        Dim mTotalQty As Double

        Dim mSerialDate As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double

        ReDim mDailyQty(31)
        Dim mCheckPartNo As String
        Dim mLastDay As Long
        Dim mLastCol As Long
        Dim FPath As String
        Dim mFileLineNo As Long
        Dim pSqlStr As String

        'Dim mNoUpdateList As New List(Of String)()

        Dim ErrorFile As System.IO.StreamWriter

        FPath = mPubBarCodePath & "\DSImportError2.txt"

        If FILEExists(FPath) Then
            Kill(FPath)
        End If

        ErrorFile = My.Computer.FileSystem.OpenTextFileWriter(FPath, True)

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " "

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()

        mLastDay = MainClass.LastDay(Month(txtScheduleDate.Text), Year(txtScheduleDate.Text))
        mLastCol = 4 + mLastDay - 1

        'FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        'FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        'strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        'FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        'If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
        '    GoTo ErrPart
        'End If

        'RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        'strWkShName = RsFile.Fields("Table_Name").Value

        'mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        'mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)

        'Import_To_Grid(strXLSFile, Extension)

        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        cntRow = 1
        mFileLineNo = 1
        For Each dtRow In dt.Rows
            mCheckItemCode = Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1)))      ''Trim(IIf(IsDBNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value))
            mCheckPartNo = Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1)))   ''Trim(IIf(IsDBNull(RsFile.Fields(2).Value), "", RsFile.Fields(2).Value))
            mCheckPartNo = Replace(mCheckPartNo, " ", "")
            mCheckPartNo = Replace(mCheckPartNo, "-", "")
            mCheckPartNo = Replace(mCheckPartNo, "/", "")
            'mCheckPartNo = Mid(mCheckPartNo, 1, 8)
            mStoreLoc = Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(3)))       ''Trim(IIf(IsDBNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value))

            mTotalQty = 0
            mWeek1Qty = 0
            mWeek2Qty = 0
            mWeek3Qty = 0
            mWeek4Qty = 0
            mWeek5Qty = 0

            I = 0
            For mFieldNo = 4 To mLastCol
                mDailyQty(I) = Format(Val(IIf(IsDBNull(dtRow.Item(mFieldNo)), 0, dtRow.Item(mFieldNo))), "0")     ''Val(IIf(IsDBNull(RsFile.Fields(mFieldNo).Value), 0, RsFile.Fields(mFieldNo).Value))

                mTotalQty = mTotalQty + mDailyQty(I)

                If I < 7 Then
                    mWeek1Qty = mWeek1Qty + mDailyQty(I)
                ElseIf I < 14 Then
                    mWeek2Qty = mWeek2Qty + mDailyQty(I)
                ElseIf I < 21 Then
                    mWeek3Qty = mWeek3Qty + mDailyQty(I)
                ElseIf I < 28 Then
                    mWeek4Qty = mWeek4Qty + mDailyQty(I)
                Else
                    mWeek5Qty = mWeek5Qty + mDailyQty(I)
                End If

                I = I + 1
            Next


            If mCheckItemCode <> "" Then
                SqlStr = " SELECT IH.AUTO_KEY_SO, IH.SO_DATE,CUST_PO_NO, IH.CUST_PO_DATE , IH.AMEND_NO, IH.AMEND_DATE, IH.AMEND_WEF_FROM, " & vbCrLf _
                            & " ID.*" & vbCrLf _
                            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID" & vbCrLf _
                            & " WHERE IH.MKEY=ID.MKEY AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf _
                            & " AND IH.AUTO_KEY_SO=" & Val(txtOurSONo.Text) & " " & vbCrLf _
                            & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y'"


                If Trim(mStoreLoc) = "" Then
                    'mSqlStr = mSqlStr & vbCrLf & " AND (ID.CUST_STORE_LOC='' OR ID.CUST_STORE_LOC IS NULL)"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND ID.CUST_STORE_LOC='" & Trim(mStoreLoc) & "' "
                End If

                SqlStr = SqlStr & " AND REPLACE(REPLACE(REPLACE(ID.PART_NO,' ',''),'-',''),'/','') = '" & mCheckPartNo & "'"

                SqlStr = SqlStr & " AND IH.ISGSTENABLE_PO='Y'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = True Then
                    RsTemp.Close()

                    SqlStr = " SELECT IH.AUTO_KEY_SO, IH.SO_DATE,CUST_PO_NO, IH.CUST_PO_DATE , IH.AMEND_NO, IH.AMEND_DATE, IH.AMEND_WEF_FROM, " & vbCrLf _
                            & " ID.*" & vbCrLf _
                            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                            & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " And IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf _
                            & " AND IH.AUTO_KEY_SO=" & Val(txtOurSONo.Text) & " " & vbCrLf _
                            & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y'"

                    If Trim(mStoreLoc) = "" Then
                        'mSqlStr = mSqlStr & vbCrLf & " AND (ID.CUST_STORE_LOC='' OR ID.CUST_STORE_LOC IS NULL)"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND ID.CUST_STORE_LOC='" & Trim(mStoreLoc) & "' "
                    End If

                    SqlStr = SqlStr & " AND REPLACE(REPLACE(REPLACE(INVMST.OLD_CUSTOMER_PART_NO,' ',''),'-',''),'/','') = '" & mCheckPartNo & "'"

                    SqlStr = SqlStr & " AND IH.ISGSTENABLE_PO='Y'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                End If
                Dim mFindItemCode As String
                Dim mFindPartNo As String
                Dim mFindUOM As String
                Dim mFindItemDesc As String = ""

                If RsTemp.EOF = False Then
                    mFindItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    mFindPartNo = IIf(IsDBNull(RsTemp.Fields("PART_NO").Value), "", RsTemp.Fields("PART_NO").Value)
                    mFindUOM = IIf(IsDBNull(RsTemp.Fields("UOM_CODE").Value), "", RsTemp.Fields("UOM_CODE").Value) '
                    mStoreSOLoc = IIf(IsDBNull(RsTemp.Fields("CUST_STORE_LOC").Value), "", RsTemp.Fields("CUST_STORE_LOC").Value)

                    If CheckDuplicateImportItem(UCase(Trim(mFindItemCode)) & "-" & UCase(Trim(mStoreSOLoc))) = True Then  ''
                        ErrorFile.WriteLine(mFileLineNo & " Duplicate Item : " & mCheckItemCode)
                        GoTo NextRecord
                    End If

                    If MainClass.ValidateWithMasterTable(mFindItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mFindItemDesc = Trim(MasterNo)
                    End If

                    If mFindUOM = "" Then
                        If MainClass.ValidateWithMasterTable(mFindItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mFindUOM = Trim(MasterNo)
                        End If
                    End If

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mFindItemCode

                    SprdMain.Col = ColCustPartNo
                    SprdMain.Text = mFindPartNo

                    SprdMain.Col = ColItemName
                    SprdMain.Text = mFindItemDesc

                    SprdMain.Col = ColItemUOM
                    SprdMain.Text = mFindUOM

                    SprdMain.Col = ColStoreLoc
                    SprdMain.Text = mStoreSOLoc     'mStoreLoc

                    SprdMain.Col = ColWeek1Qty
                    SprdMain.Text = VB6.Format(mWeek1Qty, "0.000")

                    SprdMain.Col = ColWeek2Qty
                    SprdMain.Text = VB6.Format(mWeek2Qty, "0.000")

                    SprdMain.Col = ColWeek3Qty
                    SprdMain.Text = VB6.Format(mWeek3Qty, "0.000")


                    SprdMain.Col = ColWeek4Qty
                    SprdMain.Text = VB6.Format(mWeek4Qty, "0.000")

                    SprdMain.Col = ColWeek5Qty
                    SprdMain.Text = VB6.Format(mWeek5Qty, "0.000")

                    SprdMain.Col = ColSchdQnty
                    SprdMain.Text = VB6.Format(mTotalQty, "0.000")

                    'SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                    '        & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    '        & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                    '        & " AND AUTO_KEY_DELV =" & Val(txtDSNo.Text) & "" & vbCrLf _
                    '        & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mFindItemCode) & "'" & vbCrLf _
                    '        & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    'If mStoreSOLoc <> "" Then
                    '    SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & mStoreSOLoc & "'"
                    'End If

                    'PubDBCn.Execute(SqlStr)

                    For I = 0 To 30

                        If I + 1 <= MainClass.LastDay(Month(CDate(txtScheduleDate.Text)), Year(CDate(txtScheduleDate.Text))) Then
                            mSerialDate = VB6.Format(VB6.Format(I + 1, "00") & "/" & VB6.Format(txtScheduleDate.Text, "MM/YYYY"), "DD/MM/YYYY")
                        Else
                            mSerialDate = ""
                        End If

                        If mSerialDate <> "" Then
                            pSqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET " & " (USERID, TEMP_AUTO_KEY, AUTO_KEY_DELV, " & vbCrLf _
                                    & " ITEM_CODE, SERIAL_DATE, PLANNED_QTY, " & vbCrLf _
                                    & " ACTUAL_QTY, DELV_CNT, SUPP_CUST_CODE, " & vbCrLf _
                                    & " SCHLD_DATE, REQ_DATE,LOC_CODE,BOOKTYPE ) VALUES (" & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf _
                                    & " " & Val(txtDSNo.Text) & ", " & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(mFindItemCode) & "', " & vbCrLf _
                                    & " TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                    & " " & mDailyQty(I) & ", 0, 0," & vbCrLf _
                                    & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                                    & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mStoreSOLoc & "','D') "

                            PubDBCn.Errors.Clear()
                            PubDBCn.BeginTrans()

                            PubDBCn.Execute(pSqlStr)

                            PubDBCn.CommitTrans()
                            pSqlStr = ""

                        End If
                    Next
                    cntRow = cntRow + 1
                Else
                    ErrorFile.WriteLine(mFileLineNo & " Part No Not Found : " & mCheckItemCode)
                End If
                RsTemp.Close()
                RsTemp = Nothing
            Else
                ErrorFile.WriteLine(mFileLineNo & " Part No blank :" & mCheckItemCode)
            End If
NextRecord:
            mFileLineNo = mFileLineNo + 1
        Next


        ErrorFile.Close()

        If FILEExists(FPath) Then
            Process.Start("notepad.exe", FPath)            ''Process.Start("explorer.exe", FPath)
        End If

        Exit Sub
ErrPart:
        ErrorFile.Close()
        PubDBCn.RollbackTrans()

        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Function CheckDuplicateImportItem(ByVal pCheckItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer

        Dim mCheckItemCode As String

        If pCheckItemCode = "" Then CheckDuplicateImportItem = True : Exit Function
        CheckDuplicateImportItem = False

        With SprdMain
            '.Row = pRow
            '.Col = ColItemCode
            'mItemCode = UCase(.Text)

            '.Col = ColStoreLoc
            'mItemCode = mItemCode & "-" & UCase(.Text)

            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItemCode = UCase(Trim(.Text))

                .Col = ColStoreLoc
                mCheckItemCode = mCheckItemCode & "-" & UCase(Trim(.Text))

                If UCase(mCheckItemCode) = UCase(pCheckItemCode) Then
                    CheckDuplicateImportItem = True
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub PopulateODFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mCheckItemCode As String
        Dim mODNo As String
        Dim mODDate As String
        Dim mODYear As String
        Dim mODMonth As String
        Dim mODDay As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mDailyQty As Double



        'Dim mStockType As String
        'Dim mStockQty As Double
        'Dim mAdjQty As Double
        'Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset = Nothing
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mFieldNo As Integer
        Dim I As Integer
        Dim mTotalQty As Double

        Dim mSerialDate As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mFileLineNo As Long
        'ReDim mDailyQty(30)

        SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " "


        '& vbCrLf _
        '        & " AND AUTO_KEY_DELV =" & Val(txtDSNo.Text) & "" & vbCrLf _
        '        & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        '        & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)

        'Import_To_Grid(strXLSFile, Extension)

        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        cntRow = 1
        mFileLineNo = 1
        For Each dtRow In dt.Rows

            ''If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then
            ''If RsFile.EOF = False Then
            'Do While Not RsFile.EOF
            mPartNo = Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1)))         ''Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))
            mPartNo = Replace(mPartNo, " ", "")
            mPartNo = Replace(mPartNo, "-", "")
            mPartNo = Replace(mPartNo, "/", "")
            mODNo = Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(3)))         ''Trim(IIf(IsDBNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value))
            mODDate = Trim(IIf(IsDBNull(dtRow.Item(4)), "", dtRow.Item(4)))         ''Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))
            mODYear = Year(CDate(mODDate))     ''Mid(mODDate, 1, 4)
            mODMonth = Month(CDate(mODDate))        '' Mid(mODDate, 5, 2)
            mODDay = VB6.Format(mODDate, "dd")     ''Mid(mODDate, 7, 2)


            mSerialDate = VB6.Format(mODDay & "/" & mODMonth & "/" & mODYear, "DD/MM/YYYY")

            mDailyQty = Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5)))         ''Trim(IIf(IsDBNull(RsFile.Fields(6).Value), "", RsFile.Fields(6).Value))

            mSqlStr = ""

            'If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            '    mItemCode = Trim(MasterNo)
            'Else
            '    Exit Sub
            'End If
            SqlStr = " SELECT DISTINCT ID.ITEM_CODE, ID.PART_NO,  ID.UOM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO,CUST_STORE_LOC " & vbCrLf _
                    & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                    & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y' AND  REPLACE(REPLACE(REPLACE(ID.PART_NO,' ',''),'-',''),'/','') ='" & MainClass.AllowSingleQuote(mPartNo) & "'"

            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=" & Val(lblAutoSoNo.Text) & ""

            If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
                SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
            Else
                SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
            End If

            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            I = 1
            If RsTemp.EOF = False Then
                mCheckItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            End If


            'I = 0
            'For mFieldNo = 4 To 34
            '    mDailyQty(I) = Val(IIf(IsDBNull(RsFile.Fields(mFieldNo).Value), 0, RsFile.Fields(mFieldNo).Value))
            'mTotalQty = mTotalQty + mDailyQty



            '    I = I + 1
            'Next


            If mCheckItemCode <> "" Then
                With SprdMain
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColItemCode
                        mItemCode = Trim(.Text)

                        I = 0
                        If mItemCode = mCheckItemCode Then

                            If mSerialDate <> "" Then
                                SqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET " & " (USERID, TEMP_AUTO_KEY, AUTO_KEY_DELV, " & vbCrLf _
                                        & " ITEM_CODE, SERIAL_DATE, PLANNED_QTY, " & vbCrLf _
                                        & " ACTUAL_QTY, DELV_CNT, SUPP_CUST_CODE, " & vbCrLf _
                                        & " SCHLD_DATE, REQ_DATE,OD_NO,BOOKTYPE ) VALUES (" & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf _
                                        & " " & Val(txtDSNo.Text) & ", " & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                                        & " TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                        & " " & mDailyQty & ", 0, 0," & vbCrLf _
                                        & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                                        & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mODNo) & "','D') "
                                PubDBCn.Execute(SqlStr)
                            End If

                            'Next)
                            GoTo NextRecord
                        End If
                    Next
                End With
            End If
NextRecord:
            'RsFile.MoveNext()
            '        Loop
            '    End If
            'End If
            mFileLineNo = mFileLineNo + 1
        Next

        'If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then
        '    RsFile.Close()
        '    RsFile = Nothing
        'End If

        'RsFile.Dispose()

        'If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
        '    FileDBCn.Close()
        '    FileDBCn = Nothing
        'End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                mTotalQty = 0
                mWeek1Qty = 0
                mWeek2Qty = 0
                mWeek3Qty = 0
                mWeek4Qty = 0
                mWeek5Qty = 0
                mTotalQty = 0

                SqlStr = " SELECT ITEM_CODE, SERIAL_DATE, SUM(PLANNED_QTY) AS PLANNED_QTY FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                        & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                        & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                        & " GROUP BY ITEM_CODE, SERIAL_DATE"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                I = 1
                If RsTemp.EOF = False Then
                    Do While Not RsTemp.EOF
                        mSerialDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value), "DD/MM/YYYY")
                        mDailyQty = IIf(IsDBNull(RsTemp.Fields("PLANNED_QTY").Value), 0, RsTemp.Fields("PLANNED_QTY").Value)
                        mTotalQty = mTotalQty + mDailyQty

                        If Val(VB6.Format(mSerialDate, "DD")) < 7 Then
                            mWeek1Qty = mWeek1Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 14 Then
                            mWeek2Qty = mWeek2Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 21 Then
                            mWeek3Qty = mWeek3Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 28 Then
                            mWeek4Qty = mWeek4Qty + mDailyQty
                        Else
                            mWeek5Qty = mWeek5Qty + mDailyQty
                        End If
                        RsTemp.MoveNext()
                    Loop
                End If

                .Row = cntRow
                .Col = ColWeek1Qty
                .Text = VB6.Format(mWeek1Qty, "0.000")

                .Col = ColWeek2Qty
                .Text = VB6.Format(mWeek2Qty, "0.000")

                .Col = ColWeek3Qty
                .Text = VB6.Format(mWeek3Qty, "0.000")


                .Col = ColWeek4Qty
                .Text = VB6.Format(mWeek4Qty, "0.000")

                .Col = ColWeek5Qty
                .Text = VB6.Format(mWeek5Qty, "0.000")

                .Col = ColSchdQnty
                .Text = VB6.Format(mTotalQty, "0.000")

            Next
        End With

        'RsFile.Close()

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        RsFile.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub PopulateFromXLSFile_JMD(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mCheckItemCode As String
        Dim mODNo As String
        Dim mODDate As String
        Dim mODYear As String
        Dim mODMonth As String
        Dim mODDay As String
        Dim mItemDesc As String
        Dim mUOM As String
        Dim mDailyQty As Double
        Dim mPONo As String


        'Dim mStockType As String
        'Dim mStockQty As Double
        'Dim mAdjQty As Double
        'Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset = Nothing
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mFieldNo As Integer
        Dim I As Integer
        Dim mTotalQty As Double

        Dim mSerialDate As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mFileLineNo As Long
        'ReDim mDailyQty(30)

        SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " "

        PubDBCn.Execute(SqlStr)

        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        cntRow = 1
        mFileLineNo = 1

        '        Purchasing Document	Item	Document Date	Vendor/supplying plant	Plant	Material	Short Text	qty	Net price	Delivery Date
        '9500001124  3590	12/07/2023	8521       JMD AUTO INDUSTRIES And TOOLS	1000	DC14502-500ZA	PISTON 40X22X47 LG	1	116.00	25/05/2023


        For Each dtRow In dt.Rows

            mPONo = Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1)))

            If Trim(txtPONo.Text) = mPONo Then
                mPartNo = Trim(IIf(IsDBNull(dtRow.Item(6)), "", dtRow.Item(6)))         ''Trim(IIf(IsDBNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))

                '' mODNo = Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(3)))         ''Trim(IIf(IsDBNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value))
                mODDate = Trim(IIf(IsDBNull(dtRow.Item(10)), "", dtRow.Item(10)))         ''Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))
                mSerialDate = VB6.Format(mODDate, "DD/MM/YYYY")

                mDailyQty = Trim(IIf(IsDBNull(dtRow.Item(8)), "", dtRow.Item(8)))         ''Trim(IIf(IsDBNull(RsFile.Fields(6).Value), "", RsFile.Fields(6).Value))

                mSqlStr = ""

                SqlStr = " SELECT DISTINCT ID.ITEM_CODE, ID.PART_NO,  ID.UOM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO,CUST_STORE_LOC " & vbCrLf _
                        & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                        & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                        & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " And SO_APPROVED='Y' AND  ID.PART_NO ='" & MainClass.AllowSingleQuote(mPartNo) & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=" & Val(lblAutoSoNo.Text) & ""

                SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"

                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                I = 1
                mCheckItemCode = ""
                If RsTemp.EOF = False Then
                    mCheckItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                End If

                If mCheckItemCode <> "" Then
                    With SprdMain
                        For cntRow = 1 To .MaxRows
                            .Row = cntRow
                            .Col = ColItemCode
                            mItemCode = Trim(.Text)

                            I = 0
                            If mItemCode = mCheckItemCode Then

                                If mSerialDate <> "" Then
                                    SqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET " & " (USERID, TEMP_AUTO_KEY, AUTO_KEY_DELV, " & vbCrLf _
                                            & " ITEM_CODE, SERIAL_DATE, PLANNED_QTY, " & vbCrLf _
                                            & " ACTUAL_QTY, DELV_CNT, SUPP_CUST_CODE, " & vbCrLf _
                                            & " SCHLD_DATE, REQ_DATE,OD_NO,BOOKTYPE ) VALUES (" & vbCrLf _
                                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf _
                                            & " " & Val(txtDSNo.Text) & ", " & vbCrLf _
                                            & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                                            & " TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                                            & " " & mDailyQty & ", 0, 0," & vbCrLf _
                                            & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                                            & " TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),TO_DATE('" & VB6.Format(mSerialDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mODNo) & "','D') "
                                    PubDBCn.Execute(SqlStr)
                                End If

                                'Next)
                                GoTo NextRecord
                            End If
                        Next
                    End With
                End If
            End If


NextRecord:

            mFileLineNo = mFileLineNo + 1
        Next

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                mTotalQty = 0
                mWeek1Qty = 0
                mWeek2Qty = 0
                mWeek3Qty = 0
                mWeek4Qty = 0
                mWeek5Qty = 0
                mTotalQty = 0

                SqlStr = " SELECT ITEM_CODE, SERIAL_DATE, SUM(PLANNED_QTY) AS PLANNED_QTY FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                        & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                        & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                        & " GROUP BY ITEM_CODE, SERIAL_DATE"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                I = 1
                If RsTemp.EOF = False Then
                    Do While Not RsTemp.EOF
                        mSerialDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SERIAL_DATE").Value), "", RsTemp.Fields("SERIAL_DATE").Value), "DD/MM/YYYY")
                        mDailyQty = IIf(IsDBNull(RsTemp.Fields("PLANNED_QTY").Value), 0, RsTemp.Fields("PLANNED_QTY").Value)
                        mTotalQty = mTotalQty + mDailyQty

                        If Val(VB6.Format(mSerialDate, "DD")) < 7 Then
                            mWeek1Qty = mWeek1Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 14 Then
                            mWeek2Qty = mWeek2Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 21 Then
                            mWeek3Qty = mWeek3Qty + mDailyQty
                        ElseIf Val(VB6.Format(mSerialDate, "DD")) < 28 Then
                            mWeek4Qty = mWeek4Qty + mDailyQty
                        Else
                            mWeek5Qty = mWeek5Qty + mDailyQty
                        End If
                        RsTemp.MoveNext()
                    Loop
                End If

                .Row = cntRow
                .Col = ColWeek1Qty
                .Text = VB6.Format(mWeek1Qty, "0.000")

                .Col = ColWeek2Qty
                .Text = VB6.Format(mWeek2Qty, "0.000")

                .Col = ColWeek3Qty
                .Text = VB6.Format(mWeek3Qty, "0.000")


                .Col = ColWeek4Qty
                .Text = VB6.Format(mWeek4Qty, "0.000")

                .Col = ColWeek5Qty
                .Text = VB6.Format(mWeek5Qty, "0.000")

                .Col = ColSchdQnty
                .Text = VB6.Format(mTotalQty, "0.000")

            Next
        End With

        Exit Sub
ErrPart:
        RsFile.Close()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xAcctCode As String
        Dim mOrderType As String
        Dim mPartNo As String


        If Trim(txtOurSONo.Text) = "" Then
            MsgInformation("Please enter SO NO.")
            Exit Sub
        End If

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Please Select valid Schedule Date")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            mOrderType = Trim(MasterNo)
        Else
            MsgInformation("InValid Sale Order No.")
            Exit Sub
        End If

        If DSExsistInCurrSchdMon(xAcctCode, Trim(txtOurSONo.Text), Trim(txtScheduleDate.Text), mOrderType) = True Then
            Exit Sub
        End If

        SqlStr = " SELECT DISTINCT ID.ITEM_CODE, ID.PART_NO, ID.UOM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO,CUST_STORE_LOC " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'"

        If Val(lblAutoSoNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=" & Val(lblAutoSoNo.Text) & ""
        End If

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf & " ORDER BY INVMST.CUSTOMER_PART_NO, ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then
            MainClass.ClearGrid(SprdMain, ConRowHeight)
            With SprdMain
                Do While Not RsTemp.EOF
                    .Row = I
                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                    .Col = ColCustPartNo
                    mPartNo = IIf(IsDbNull(RsTemp.Fields("PART_NO").Value), "", RsTemp.Fields("PART_NO").Value)
                    mPartNo = IIf(mPartNo = "", IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value), mPartNo)

                    .Text = mPartNo ''IIf(IsNull(RsTemp!PART_NO), "", RsTemp!PART_NO) '' IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)

                    .Col = ColItemName
                    .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                    .Col = ColItemUOM
                    .Text = IIf(IsDbNull(RsTemp.Fields("UOM_CODE").Value), "", RsTemp.Fields("UOM_CODE").Value)

                    .Col = ColStoreLoc
                    .Text = IIf(IsDBNull(RsTemp.Fields("CUST_STORE_LOC").Value), "", RsTemp.Fields("CUST_STORE_LOC").Value)

                    I = I + 1
                    .MaxRows = I
                    RsTemp.MoveNext()
                Loop
            End With
        End If

        FormatSprdMain(-1)
        txtCode.Enabled = False
        txtSupplierName.Enabled = False
        txtOurSONo.Enabled = False
        cmdPoSearch.Enabled = False
        txtScheduleDate.Enabled = False
        Exit Sub

ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPoSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPoSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim pSuppCode As String

        If (txtSupplierName.Text) = "" Then
            MsgInformation("Please Enter Valid Supplier Name")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pSuppCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCode & "' AND SO_STATUS='O' AND SO_APPROVED='Y'"

        '    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        '
        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        If MainClass.SearchGridMaster((txtOurSONo.Text), "DSP_SALEORDER_HDR", "AUTO_KEY_SO", "AMEND_NO", "CUST_PO_NO", "CUST_PO_DATE", SqlStr) = True Then
            txtPOAmendNo.Text = AcName1
            txtOurSONo.Text = AcName
            lblAutoSoNo.Text = Val(txtOurSONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000")
            txtOurSONo_Validating(txtOurSONo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click

        On Error GoTo ModifyErr
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim mItemCode As String
        Dim mNewItemCode As String
        Dim mItemCodeExsits As Boolean
        Dim mPartNo As String

        '    If mAmendSchd = False Then Exit Sub

        If Trim(txtOurSONo.Text) = "" Then
            MsgInformation("Please enter SO NO.")
            Exit Sub
        End If

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Please Select valid Schedule Date")
            Exit Sub
        End If

        If MsgQuestion("Want to reset Lastest Purchase Order No.? ") = CStr(MsgBoxResult.Yes) Then

            SqlStr = " SELECT AUTO_KEY_SO, SO_DATE,CUST_PO_NO, CUST_PO_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_FROM " & vbCrLf _
                & " FROM DSP_SALEORDER_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "' " & vbCrLf _
                & " AND AUTO_KEY_SO=" & Val(txtOurSONo.Text) & " " & vbCrLf _
                & " AND SO_STATUS='O' AND SO_APPROVED='Y'"

            If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
                SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
            Else
                SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtPOAmendNo.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_NO").Value), 0, RsTemp.Fields("AMEND_NO").Value)
                lblAutoSoNo.Text = Val(txtOurSONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000")
                txtPOAmendDate.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_DATE").Value), "", RsTemp.Fields("AMEND_DATE").Value)
                txtWEF.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_WEF_FROM").Value), "", RsTemp.Fields("AMEND_WEF_FROM").Value)

                txtPODate.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value)
                txtPONo.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
                lblAutoSodate.Text = IIf(IsDbNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)
            End If
        End If


        SqlStr = " SELECT DISTINCT ID.ITEM_CODE, ID.PART_NO, ID.UOM_CODE, INVMST.ITEM_SHORT_DESC,INVMST.CUSTOMER_PART_NO,CUST_STORE_LOC " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'"

        '' AND IH.SO_STATUS='O'

        If Val(lblAutoSoNo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY=" & Val(lblAutoSoNo.Text) & ""
        End If

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf & " ORDER BY INVMST.CUSTOMER_PART_NO, ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            With SprdMain
                Do While Not RsTemp.EOF
                    mItemCodeExsits = False
                    mNewItemCode = MainClass.AllowSingleQuote(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    For I = 1 To .MaxRows - 1
                        .Row = I
                        .Col = ColItemCode
                        mItemCode = MainClass.AllowSingleQuote(.Text)
                        If mNewItemCode = mItemCode Then
                            mItemCodeExsits = True
                            Exit For
                        End If
                    Next

                    If mItemCodeExsits = False Then
                        .Row = .MaxRows
                        .Col = ColItemCode
                        .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                        .Col = ColCustPartNo
                        mPartNo = IIf(IsDbNull(RsTemp.Fields("PART_NO").Value), "", RsTemp.Fields("PART_NO").Value)
                        mPartNo = IIf(mPartNo = "", IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value), mPartNo)
                        .Text = mPartNo ''IIf(IsNull(RsTemp!PART_NO), "", RsTemp!PART_NO)   '' IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)

                        .Col = ColItemName
                        .Text = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                        .Col = ColItemUOM
                        .Text = IIf(IsDbNull(RsTemp.Fields("UOM_CODE").Value), "", RsTemp.Fields("UOM_CODE").Value)

                        .Col = ColStoreLoc
                        .Text = IIf(IsDBNull(RsTemp.Fields("CUST_STORE_LOC").Value), "", RsTemp.Fields("CUST_STORE_LOC").Value)

                        .MaxRows = .MaxRows + 1
                    End If
                    RsTemp.MoveNext()
                Loop
            End With
        End If

        FormatSprdMain(-1)

        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
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
        Dim SqlStr As String = ""
        Dim mDSNo As Double
        Dim mScheduleStatus As String
        Dim mApprovalBH As String
        Dim mApprovalPH As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mScheduleStatus = VB.Left(cboStatus.Text, 1)
        mApprovalBH = IIf(chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mApprovalPH = IIf(chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        SqlStr = ""
        mDSNo = Val(txtDSNo.Text)
        If Val(txtDSNo.Text) = 0 Then
            mDSNo = AutoGenPONoSeq()
        End If
        txtDSNo.Text = CStr(mDSNo)


        If ADDMode = True Then
            lblMkey.Text = CStr(mDSNo)



            SqlStr = " INSERT INTO DSP_DELV_SCHLD_REQ_HDR ( " & vbCrLf _
                & " COMPANY_CODE , AUTO_KEY_DELV," & vbCrLf _
                & " DELV_SCHLD_DATE ,  CUST_DELV_NO," & vbCrLf _
                & " CUST_DELV_DATE , AUTO_KEY_SO," & vbCrLf _
                & " SO_DATE , CUST_SO_NO," & vbCrLf _
                & " CUST_SO_DATE , SO_AMEND_NO," & vbCrLf _
                & " AMEND_DATE , AMEND_WEF_DATE," & vbCrLf _
                & " SUPP_CUST_CODE , SCHLD_DATE," & vbCrLf _
                & " DELV_AMEND_NO , DELV_AMEND_DATE, " & vbCrLf _
                & " SCHLD_STATUS , REMARKS, IS_MAIL, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,APPROVAL_BH, APPROVAL_PH) "


            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & " , " & mDSNo & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtCustDSNo.Text)) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtCustDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & Val(txtOurSONo.Text) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(lblAutoSodate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , '" & MainClass.AllowSingleQuote((txtPONo.Text)) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & Val(txtPOAmendNo.Text) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPOAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtCode.Text)) & "' , TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(txtDSAmendNo.Text) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mScheduleStatus & "' , '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', 'N', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N','N')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE DSP_DELV_SCHLD_REQ_HDR SET " & vbCrLf _
                & " AUTO_KEY_DELV=" & mDSNo & "," & vbCrLf _
                & " DELV_SCHLD_DATE=TO_DATE('" & VB6.Format(txtDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " CUST_DELV_NO='" & MainClass.AllowSingleQuote((txtCustDSNo.Text)) & "'," & vbCrLf _
                & " CUST_DELV_DATE=TO_DATE('" & VB6.Format(txtCustDSDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " DELV_AMEND_NO=" & Val(txtDSAmendNo.Text) & ", " & vbCrLf _
                & " DELV_AMEND_DATE=TO_DATE('" & VB6.Format(txtDSAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AUTO_KEY_SO=" & Val(txtOurSONo.Text) & "," & vbCrLf _
                & " SO_DATE=TO_DATE('" & VB6.Format(lblAutoSodate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " CUST_SO_NO='" & MainClass.AllowSingleQuote((txtPONo.Text)) & "'," & vbCrLf _
                & " CUST_SO_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " SO_AMEND_NO=" & Val(txtPOAmendNo.Text) & "," & vbCrLf _
                & " AMEND_DATE=TO_DATE('" & VB6.Format(txtPOAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "' , " & vbCrLf _
                & " SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " SCHLD_STATUS='" & mScheduleStatus & "' , " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', IS_MAIL='N', " & vbCrLf _
                & " APPROVAL_BH='" & mApprovalBH & "', APPROVAL_PH='" & mApprovalPH & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND AUTO_KEY_DELV =" & Val(CStr(mDSNo)) & ""

        End If
        ''mApprovalBH

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1 = False Then GoTo ErrPart
        If UpdateDailyDSDetail = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtDSNo.Text = CStr(mDSNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsDSSMain.Requery()
        RsDSSDetail.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function AutoGenPONoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DELV)  " & vbCrLf & " FROM DSP_DELV_SCHLD_REQ_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
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
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemUOM As String
        Dim mWeek1Qty As Double
        Dim mWeek2Qty As Double
        Dim mWeek3Qty As Double
        Dim mWeek4Qty As Double
        Dim mWeek5Qty As Double
        Dim mTotQty As Double
        Dim mAmendNO As Integer
        Dim mAmendReason As String
        Dim mStoreLoc As String

        If DeleteDSDailyDetail(PubDBCn, Val(lblMkey.Text)) = False Then GoTo UpdateDetail1

        SqlStr = "Delete From  DSP_DELV_SCHLD_REQ_DET " & vbCrLf & " Where " & vbCrLf & " AUTO_KEY_DELV=" & Val(lblMkey.Text) & "" & vbCrLf

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColStoreLoc
                mStoreLoc = Trim(.Text)

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

                .Col = ColSchdQnty
                mTotQty = Val(.Text)

                .Col = ColAmendNo
                mAmendNO = Val(.Text) ''Val(txtDSAmendNo.Text)         ''

                .Col = ColAmendReason
                mAmendReason = Trim(.Text)

                SqlStr = ""

                '            If mItemCode <> "" And mTotQty > 0 Then
                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO DSP_DELV_SCHLD_REQ_DET ( " & vbCrLf _
                        & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                        & " ITEM_UOM, " & vbCrLf _
                        & " WEEK1_QTY, WEEK2_QTY, " & vbCrLf _
                        & " WEEK3_QTY, WEEK4_QTY, " & vbCrLf _
                        & " WEEK5_QTY, " & vbCrLf _
                        & " ITEM_QTY, AMEND_NO, COMPANY_CODE, AMEND_REASON, LOC_CODE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf _
                        & " " & mWeek1Qty & ", " & mWeek2Qty & ", " & vbCrLf _
                        & " " & mWeek3Qty & "," & mWeek4Qty & "," & mWeek5Qty & ", " & vbCrLf _
                        & " " & mTotQty & ", " & mAmendNO & ", " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(mAmendReason) & "','" & mStoreLoc & "') "

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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtSupplierName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
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
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCode.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
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
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            'FormatSprdView()
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default

        'Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'If CmdView.Text = ConCmdGridViewCaption Then
        '    CmdView.Text = ConCmdViewCaption
        '    MainClass.ClearGrid(SprdView)
        '    AssignGrid(True)
        '    '        ADataGrid.Refresh
        '    FormatSprdView()
        '    SprdView.Refresh()
        '    SprdView.Focus()
        '    SprdView.BringToFront()
        'Else
        '    CmdView.Text = ConCmdGridViewCaption
        '    SprdView.SendToBack()
        'End If
        'MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        'Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmSalesDSRequisition_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sale Delivery Schedule Requisition"

        SqlStr = "Select * From DSP_DELV_SCHLD_REQ_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From DSP_DELV_SCHLD_REQ_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        cboStatus.Items.Clear()
        cboStatus.Items.Add("Open")
        cboStatus.Items.Add("Close")

        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""


        SqlStr = " SELECT " & vbCrLf _
            & " A.AUTO_KEY_DELV AS DSNo, A.DELV_SCHLD_DATE As DS_DATE, " & vbCrLf _
            & " A.CUST_DELV_NO AS CUST_DS, A.CUST_DELV_DATE AS CUST_DSDATE,  A.SUPP_CUST_CODE, " & vbCrLf _
            & " C.SUPP_CUST_NAME AS NAME, A.CUST_SO_NO AS PO_NO, " & vbCrLf _
            & " A.SCHLD_DATE, DECODE(A.SCHLD_STATUS,'O','OPEN','CLOSE') AS Status, B.ITEM_CODE, INVMST.CUSTOMER_PART_NO, B.ITEM_UOM, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " B.ITEM_QTY, B.WEEK1_QTY, B.WEEK2_QTY, B.WEEK3_QTY, B.WEEK4_QTY, B.WEEK5_QTY, B.LOC_CODE, A.REMARKS " & vbCrLf _
            & " FROM DSP_DELV_SCHLD_REQ_HDR A, DSP_DELV_SCHLD_REQ_DET B, FIN_SUPP_CUST_MST C, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE A.AUTO_KEY_DELV=B.AUTO_KEY_DELV " & vbCrLf _
            & " AND A.COMPANY_CODE=C.COMPANY_CODE " & vbCrLf _
            & " And A.SUPP_CUST_CODE=C.SUPP_CUST_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " And B.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " And A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And SUBSTR(A.AUTO_KEY_DELV,LENGTH(A.AUTO_KEY_DELV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & " ORDER BY A.AUTO_KEY_DELV"


        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""

        CreateGridHeader("S")


        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "DS No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "DS Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Customer Delviery No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Customer Delivery Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Customer Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Customer PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Schedule Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Customer Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Item UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Item Name"

            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Item Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Week 1 Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Week 2 Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Week 3 Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Week 4 Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Header.Caption = "Week 5 Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(19).Header.Caption = "Store Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(20).Header.Caption = "Remarks"

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Style = UltraWinGrid.ColumnStyle.Double
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).CellAppearance.TextHAlign = HAlign.Right

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100

            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(19).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(20).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub frmSalesDSRequisition_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

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
        txtCustDSNo.Text = CStr(0)
        txtCustDSDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtDSAmendNo.Text = CStr(0)
        txtDSAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtSupplierName.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        cmdsearch.Enabled = True
        txtSupplierName.Enabled = True
        SprdMain.Enabled = True

        txtPONo.Text = ""
        txtOurSONo.Text = ""
        txtPONo.Enabled = False
        txtOurSONo.Enabled = True
        txtScheduleDate.Enabled = True
        txtPODate.Text = ""
        txtPOAmendNo.Text = ""
        txtPOAmendDate.Text = ""
        txtScheduleDate.Text = "01/" & VB6.Format(Month(RunDate), "00") & "/" & VB6.Format(Year(RunDate), "0000")
        txtScheduleDate.Enabled = True
        cboStatus.SelectedIndex = 0
        txtRemarks.Text = ""

        txtDSAmendNo.Enabled = False
        txtDSAmendDate.Enabled = False

        chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApprovalBH.Enabled = False
        chkApprovalPH.Enabled = False

        '    cmdAmendSchd.Enabled = False

        '    txtCustDSNo.Enabled = False
        '    txtCustDSDate.Enabled = False

        cmdPoSearch.Enabled = True

        lblAutoSoNo.Text = ""
        lblAutoSodate.Text = ""

        cmdPopulate.Enabled = False

        cboStatus.Enabled = True
        mAmendSchd = False

        '    Call DelTemp_DailyDetail
        pTempSeq = MainClass.AutoGenRowNo("DSP_DAILY_SCHLD_DET", "RowNo", PubDBCn)

        Call DelTemp_DailyDetail()

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDSSDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 7)


            .Col = ColCustPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 6)
            .TypeEditMultiLine = True


            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 18)
            '        .ColUserSortIndicator(ColItemName) = ColUserSortIndicatorAscending
            .TypeEditMultiLine = True

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsDSSDetail.Fields("ITEM_UOM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsDSSDetail.Fields("LOC_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(.Col, 4)


            .Col = ColItemDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColItemDetail, 4.5)

            .ColsFrozen = ColItemDetail

            .Col = ColPreviousQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2 '' 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSSDetail.Fields("ITEM_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            For cntCol = ColWeek1Qty To ColWeek5Qty
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2 ''4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 7)
            Next

            .Col = ColSchdQnty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2 '' 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsDSSDetail.Fields("ITEM_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsDSSDetail.Fields("AMEND_NO").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 4)
            .ColHidden = False

            .Col = ColAmendReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDSSDetail.Fields("AMEND_REASON").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 18)
            '        .ColUserSortIndicator(ColItemName) = ColUserSortIndicatorAscending
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemUOM)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPreviousQty, ColSchdQnty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmendNo, ColAmendNo)
            MainClass.SetSpreadColor(SprdMain, Arow)
            '        .Col = ColItemName
            '        .UserColAction = UserColActionSort
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1
    '        .set_RowHeight(0, 300)
    '        .set_ColWidth(0, 500)
    '        .set_ColWidth(1, 1200)
    '        .set_ColWidth(2, 1200)
    '        .set_ColWidth(3, 1500)
    '        .set_ColWidth(4, 1500)
    '        .set_ColWidth(5, 3500)
    '        .set_ColWidth(6, 1200)
    '        .set_ColWidth(7, 1200)
    '        .set_ColWidth(8, 1000)
    '        .set_ColWidth(9, 2000)
    '        .ColsFrozen = 2
    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtDSNo.Maxlength = RsDSSMain.Fields("AUTO_KEY_DELV").Precision
        txtDSDate.Maxlength = 10
        txtSupplierName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)


        txtCustDSNo.Maxlength = RsDSSMain.Fields("CUST_DELV_NO").DefinedSize
        txtCustDSDate.Maxlength = 10

        txtSupplierName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.Maxlength = RsDSSMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtDSAmendNo.Maxlength = RsDSSMain.Fields("DELV_AMEND_NO").Precision
        txtDSAmendDate.Maxlength = RsDSSMain.Fields("DELV_AMEND_DATE").DefinedSize - 6

        txtPONo.Maxlength = RsDSSMain.Fields("CUST_SO_NO").DefinedSize
        txtPODate.Maxlength = 10
        txtPOAmendNo.Maxlength = RsDSSMain.Fields("SO_AMEND_NO").Precision
        txtPOAmendDate.Maxlength = 10
        txtScheduleDate.Maxlength = 10

        txtWEF.Maxlength = 10
        txtRemarks.Maxlength = RsDSSMain.Fields("REMARKS").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mTotQty As Double
        Dim I As Integer
        Dim mItemCode As String
        Dim mSOValidQty As Double
        Dim mPreviousQty As Double
        Dim mOrderType As String
        Dim mSOMKey As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mPreviousQty As Double
        Dim mAmendReason As String
        Dim mStoreLoc As String

        FieldsVarification = True
        If ValidateBranchLocking((txtScheduleDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateBookLocking(PubDBCn, CInt(ConLockSO_DS), txtScheduleDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, (txtScheduleDate.Text), (txtSupplierName.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If


        '    If MODIFYMode = True Then
        '        If RsDSSMain!POST_FLAG = "Y" Then
        '            MsgInformation "Posted DS Cann't be Modified"
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If

        If chkApprovalBH.CheckState = System.Windows.Forms.CheckState.Checked Or chkApprovalPH.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Approval Delivery Schedule Cann't be Changed")
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And RsDSSMain.EOF = True Then Exit Function

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
            MsgInformation("Customer Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCustDSNo.Text) = "" Then
            MsgInformation("Customer D.S. No is Blank. Cannot Save")
            If txtCustDSNo.Enabled = True Then txtCustDSNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Customer Name is not a Supplier or Customer Category. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
        '        MsgInformation "Customer Name is not a Supplier or Customer Category. Cannot Save"
        '        If txtSupplierName.Enabled = True Then txtSupplierName.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If VB6.Format(txtScheduleDate.Text, "YYYYMM") < VB6.Format(txtDSDate.Text, "YYYYMM") Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            txtScheduleDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mOrderType = "O"
        If MainClass.ValidateWithMasterTable(Val(txtOurSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        Else
            MsgInformation("Invalid Our Sales Order No. Cannot Save")
            '        If txtSupplierName.Enabled = True Then txtSupplierName.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        SqlStr = " SELECT AUTO_KEY_SO, SO_DATE,CUST_PO_NO, CUST_PO_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_FROM " & vbCrLf & " FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(lblAutoSoNo.Text) & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "' AND SO_APPROVED='Y'" 'AND MKEY=" & Val(lblAutoSoNo.text) & " ''Change with SO No on Dated : 28/11/2018

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Please Select Sales Order No in GST regime. Cannot Save")
            '        If txtSupplierName.Enabled = True Then txtSupplierName.SetFocus
            FieldsVarification = False
            Exit Function
        End If

        If DSExsistInCurrSchdMon(Trim(txtCode.Text), Trim(txtOurSONo.Text), Trim(txtScheduleDate.Text), mOrderType) = True Then
            FieldsVarification = False
            Exit Function
        End If

        For I = 1 To SprdMain.MaxRows - 1
            mSOValidQty = 0
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))

            SprdMain.Col = ColPreviousQty
            mPreviousQty = Val(SprdMain.Text)

            SprdMain.Col = ColSchdQnty
            mTotQty = Val(SprdMain.Text)

            SprdMain.Col = ColAmendReason
            mAmendReason = Trim(SprdMain.Text)

            If Val(txtDSAmendNo.Text) > 0 Then
                If mPreviousQty <> mTotQty Then
                    If Len(mAmendReason) < 5 Then
                        MsgInformation("Please Enter the Amend valid reason for Item Code :" & mItemCode)
                        MainClass.SetFocusToCell(SprdMain, I, ColAmendReason)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            SprdMain.Col = ColStoreLoc
            mStoreLoc = Trim(SprdMain.Text)

            mSOMKey = Val(txtOurSONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000")
            If MainClass.ValidateWithMasterTable(mSOMKey, "MKEY", "VALID_QTY", "DSP_SALEORDER_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'  AND CUST_STORE_LOC='" & mStoreLoc & "'") = True Then
                mSOValidQty = Val(MasterNo)
            End If

            If mSOValidQty > 0 Then
                '            mPreviousQty = GetPreviousDSQty(Val(txtOurSONo.Text))
                If mSOValidQty < mTotQty Then
                    MsgInformation("PO is valid for Only " & mSOValidQty & " For Item Code :" & mItemCode)
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            ''Temp Mark
            If mItemCode <> "" And mTotQty > 0 Then
                If CheckDSDetailExists(mItemCode, mStoreLoc, I, mTotQty) = False Then
                    MsgInformation("Please Check Delivery Detail Qty. For Item Code :" & mItemCode)
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

        Next

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False

        '    If MainClass.ValidDataInGrid(SprdMain, ColTotQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Function CheckDSDetailExists(ByRef nItemCode As String, ByRef mStoreLoc As String, ByRef mSerialNo As Integer, ByRef mDSQty As Double) As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset

        SqlStr = "SELECT SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf _
            & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
            & " AND ITEM_CODE='" & Trim(nItemCode) & "'"

        If mStoreLoc = "" Then
            SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & Trim(mStoreLoc) & "' OR LOC_CODE IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & Trim(mStoreLoc) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ITEM_CODE "


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
    Private Sub frmSalesDSRequisition_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsDSSMain.Close()
        'RsOpOuts.Close
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked


        If ADDMode = False And MODIFYMode = False Then Exit Sub

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call ShowFormDSDailyDetail(eventArgs.Col, eventArgs.Row)
    End Sub
    Private Sub ShowFormDSDailyDetail(ByRef pCol As Integer, ByRef pRow As Integer)
        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim pDate As String
        Dim mItemCode As String = ""
        'Dim mItemName As String
        'Dim mQty As String
        Dim mOrderType As String = ""
        Dim mSchdQty As Double
        Dim mStoreLoc As String = ""
        Dim mDeliveryInstruction As String = "N"

        ''txtOurSONo

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text

            .Col = ColStoreLoc
            mStoreLoc = .Text

            .Col = ColSchdQnty
            mSchdQty = Val(.Text)
        End With
        If mItemCode = "" Then Exit Sub

        If Trim(txtScheduleDate.Text) = "" Then
            MsgInformation("Please Enter Valid Schedule Date")
            txtScheduleDate.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "ORDER_TYPE", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            mOrderType = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtOurSONo.Text), "AUTO_KEY_SO", "DELIVERY_INSTRUCTION_REQ", "DSP_SALEORDER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SO_APPROVED='Y'") = True Then
            mDeliveryInstruction = MasterNo
        End If


        ConSaleDSDetail = False '' frmSalesDS.lblDetail.Text = "False"
        If mOrderType = "O" And mDeliveryInstruction = "N" Then ''If mOrderType = "O" Then
            With FrmSalesDSDailyDetail
                .LblAddMode.Text = CStr(ADDMode)
                .LblModifyMode.Text = CStr(MODIFYMode)
                .LblTempSeq.Text = CStr(Val(pTempSeq))
                .lblPoNo.Text = CStr(Val(txtDSNo.Text))
                .lblItemCode.Text = mItemCode
                .lblSuppCode.Text = txtCode.Text
                .lblStoreLoc.Text = mStoreLoc
                .lblDI.Text = mDeliveryInstruction
                .LblPODate.Text = VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY")
                .lblScheQty.Text = VB6.Format(mSchdQty, "0.00")
                .lblMainActiveRow.Text = CStr(pRow)
                .lblBookType.Text = "D"
                .ShowDialog()
            End With

            If ConSaleDSDetail = True Then ''If frmSalesDS.lblDetail.Text = "True" Then
                With SprdMain
                    .Row = pRow
                    .Col = ColWeek1Qty
                    .Text = CStr(Val(FrmSalesDSDailyDetail.lblWeek1.Text))
                    .Col = ColWeek2Qty
                    .Text = CStr(Val(FrmSalesDSDailyDetail.lblWeek2.Text))
                    .Col = ColWeek3Qty
                    .Text = CStr(Val(FrmSalesDSDailyDetail.lblWeek3.Text))
                    .Col = ColWeek4Qty
                    .Text = CStr(Val(FrmSalesDSDailyDetail.lblWeek4.Text))
                    .Col = ColWeek5Qty
                    .Text = CStr(Val(FrmSalesDSDailyDetail.lblWeek5.Text))
                    FrmSalesDSDailyDetail.Close()
                End With
                Call CalcTots()
            End If
        Else
            With FrmSalesDSDailyClosed
                .LblAddMode.Text = CStr(ADDMode)
                .LblModifyMode.Text = CStr(MODIFYMode)
                .LblTempSeq.Text = CStr(Val(pTempSeq))
                .lblPoNo.Text = CStr(Val(txtDSNo.Text))
                .lblItemCode.Text = mItemCode
                .lblSuppCode.Text = txtCode.Text
                .lblStoreLoc.Text = mStoreLoc
                .lblDI.Text = mDeliveryInstruction
                .LblPODate.Text = VB6.Format(txtScheduleDate.Text, "DD/MM/YYYY")
                '                .lblScheQty.text = Format(mSchdQty, "0.00")
                .lblMainActiveRow.Text = CStr(pRow)
                .lblBookType.Text = "D"
                .ShowDialog()
            End With

            If ConSaleDSDetail = True Then ''If frmSalesDS.lblDetail.Text = "True" Then
                With SprdMain
                    .Row = pRow
                    .Col = ColWeek1Qty
                    .Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek1.Text))
                    .Col = ColWeek2Qty
                    .Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek2.Text))
                    .Col = ColWeek3Qty
                    .Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek3.Text))
                    .Col = ColWeek4Qty
                    .Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek4.Text))
                    .Col = ColWeek5Qty
                    .Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek5.Text))
                    FrmSalesDSDailyClosed.Close()
                End With
                Call CalcTots()
            End If
        End If
    End Sub
    Private Sub ShowDSDailyDetail()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_DailyDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET ( " & vbCrLf _
            & " UserId, TEMP_AUTO_KEY, AUTO_KEY_DELV, ITEM_CODE, " & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY," & vbCrLf _
            & " DELV_CNT, SUPP_CUST_CODE,SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE)" & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf _
            & " AUTO_KEY_DELV, ITEM_CODE," & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
            & " DELV_CNT , SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE " & vbCrLf _
            & " FROM DSP_DAILY_SCHLD_REQ_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(lblMkey.Text) & "  AND BOOKTYPE='D'" & vbCrLf _
            & " ORDER BY SERIAL_NO, SERIAL_DATE"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_DailyDetail(Optional ByRef mRefNo As String = "", Optional ByRef mItemCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        SqlStr = SqlStr & vbCrLf & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " "

        If mRefNo <> "" And mItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_DELV=" & Val(mRefNo) & "' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub

    Private Function InsertIntoTempTable() As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pItemCode As String
        Dim xCustomerCode As String = ""
        Dim pUOM As String
        Dim pRate As Double
        Dim pQty As Double
        Dim pAmount As Double
        Dim mSaleRep As String
        Dim mEMailID As String

        InsertIntoTempTable = False
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mSqlStr = "DELETE FROM TEMP_DS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(mSqlStr)

        mSqlStr = " INSERT INTO TEMP_DS ( " & vbCrLf & " USER_ID, COMPANY_CODE, SUPP_CUST_CODE, " & vbCrLf & " SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf & " SUPP_CUST_STATE, SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf & " SUPP_CUST_FAXNO, SUPP_CUST_MAILID, SUPP_CUST_MOBILE, CONTACT_TELNO," & vbCrLf & " ITEM_CODE, ITEM_SHORT_DESC, CUSTOMER_PART_NO, " & vbCrLf & " AUTO_KEY_DELV, DELV_SCHLD_DATE, CUST_DELV_NO, " & vbCrLf & " CUST_DELV_DATE, AUTO_KEY_SO, SO_DATE, " & vbCrLf & " CUST_SO_NO, CUST_SO_DATE, SERIAL_NO, " & vbCrLf & " SERIAL_DATE, PLANNED_QTY, REQ_DATE,REMARKS,RATE, AMOUNT,PACK_STD " & vbCrLf & " ) "


        mSqlStr = mSqlStr & vbCrLf & " SELECT " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', CMST.COMPANY_CODE, CMST.SUPP_CUST_CODE, " & vbCrLf & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, " & vbCrLf & " CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, CMST.SUPP_CUST_MOBILE, CMST.CONTACT_TELNO, " & vbCrLf & " INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " IH.AUTO_KEY_DELV, IH.DELV_SCHLD_DATE, IH.CUST_DELV_NO, " & vbCrLf & " IH.CUST_DELV_DATE, IH.AUTO_KEY_SO, IH.SO_DATE, " & vbCrLf & " IH.CUST_SO_NO, IH.CUST_SO_DATE, ID.SERIAL_NO, " & vbCrLf & " ID.SERIAL_DATE, ID.PLANNED_QTY, ID.REQ_DATE, REMARKS, 0, 0,INVMST.PACK_STD "

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DAILY_SCHLD_REQ_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV=" & Val(txtDSNo.Text) & " AND ID.PLANNED_QTY>0"

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        PubDBCn.Execute(mSqlStr)

        mSqlStr = "SELECT * FROM TEMP_DS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        Do While RsTemp.EOF = False

            pItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            xCustomerCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            pRate = GetMRPRate(pItemCode, xCustomerCode)

            mSqlStr = " UPDATE TEMP_DS SET RATE=" & pRate & "" & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xCustomerCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            PubDBCn.Execute(mSqlStr)
            RsTemp.MoveNext()
        Loop

        mEMailID = ""
        mSaleRep = GetSaleRep(xCustomerCode, mEMailID)

        mSqlStr = " UPDATE TEMP_DS SET CONTACT_TELNO='" & MainClass.AllowSingleQuote(mSaleRep) & "', " & vbCrLf & " SUPP_CUST_MAILID='" & MainClass.AllowSingleQuote(mEMailID) & "' " & vbCrLf & " WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xCustomerCode) & "'"

        PubDBCn.Execute(mSqlStr)


        PubDBCn.CommitTrans()

        InsertIntoTempTable = True
        Exit Function
ErrPart:
        InsertIntoTempTable = False
        PubDBCn.RollbackTrans()
    End Function

    Private Function GetMRPRate(ByRef pItemCode As String, ByRef xCustomerCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mWithInCountry As String



        GetMRPRate = 0
        SqlStr = "SELECT ITEM_RATE ,ITEM_RATE_F, WITHIN_COUNTRY  FROM FIN_SUPP_CUST_DET A, FIN_SUPP_CUST_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE='" & xCustomerCode & "' AND A.ITEM_CODE='" & pItemCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If RsTemp.Fields("WITHIN_COUNTRY").Value = "Y" Then
                GetMRPRate = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))
            Else
                GetMRPRate = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE_F").Value), 0, RsTemp.Fields("ITEM_RATE_F").Value))
            End If
            '            SqlStr = "SELECT PURCHASE_UOM,UOM_FACTOR " & vbCrLf _
            ''                    & " FROM INV_ITEM_MST" & vbCrLf _
            ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            ''                    & " AND ITEM_CODE='" & pItemCode & "'"
            '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
            '            If RsTemp.EOF = False Then
            '                mPurchaseUOM = Val(IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM))
            '                mFactor = Val(IIf(IsNull(RsTemp!UOM_FACTOR), 0, RsTemp!UOM_FACTOR))
            '                If Trim(mPurchaseUOM) <> Trim(pUOM) Then
            '                    GetMRPRate = Format(GetMRPRate / IIf(mFactor = 0, 1, mFactor), "0.0000")
            '                End If
            '            End If
        Else
            GetMRPRate = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetMRPRate = 0
    End Function

    Private Function GetSaleRep(ByRef xCustomerCode As String, ByRef mEMailID As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mEmpCode As String


        GetSaleRep = ""
        mEmpCode = ""
        mEMailID = ""
        SqlStr = "SELECT EMP_CODE FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xCustomerCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mEmpCode = CStr(Val(IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), 0, RsTemp.Fields("EMP_CODE").Value)))
        End If

        If mEmpCode <> "" Then
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                GetSaleRep = MasterNo
            End If
            If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mEMailID = MasterNo
            End If
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetSaleRep = ""
    End Function

    Private Function SelectQryForDailyDS(ByRef mSqlStr As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = SqlStr & vbCrLf & " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,CMST.SUPP_CUST_NAME"

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM DSP_DELV_SCHLD_REQ_HDR IH, DSP_DAILY_SCHLD_REQ_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DELV=" & Val(txtDSNo.Text) & "" & vbCrLf

        ''ORDER CLAUSE...

        SqlStr = SqlStr & vbCrLf & "ORDER BY ID.SERIAL_DATE"

        SelectQryForDailyDS = SqlStr
        Exit Function
ErrPart:

    End Function

    Private Function UpdateDailyDSDetail() As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mStoreLoc As String

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColStoreLoc
                mStoreLoc = Trim(.Text)

                SqlStr = "INSERT INTO DSP_DAILY_SCHLD_REQ_DET (" & vbCrLf _
                    & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE )" & vbCrLf _
                    & " SELECT " & vbCrLf _
                    & " " & Val(txtDSNo.Text) & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                    & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE " & vbCrLf _
                    & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND BOOKTYPE='D'"

                If mStoreLoc = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "' OR LOC_CODE IS NULL)"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



                PubDBCn.Execute(SqlStr)

                SqlStr = "INSERT INTO DSP_DAILY_SCHLD_REQ_LOG_DET (" & vbCrLf _
                    & " AUTO_KEY_DELV, AMEND_NO, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY, LOC_CODE, OD_NO,BOOKTYPE,MODUSER, MODDATE)" & vbCrLf _
                    & " SELECT " & vbCrLf _
                    & " " & Val(txtDSNo.Text) & ", " & VB6.Format(txtDSAmendNo.Text, "000") & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                    & " SERIAL_DATE, PLANNED_QTY,LOC_CODE, OD_NO, BOOKTYPE,'" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                    & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                    & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND BOOKTYPE='D'"

                If mStoreLoc = "" Then
                    SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "' OR LOC_CODE IS NULL)"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        If Trim(pTempSeq) <> "" Then
            SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET WHERE TEMP_AUTO_KEY=" & Val(pTempSeq) & ""
            PubDBCn.Execute(SqlStr)
        End If

        UpdateDailyDSDetail = True
        Exit Function
UpdateErr1:
        'Resume
        UpdateDailyDSDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteDSDailyDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As Double) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteDSDailyDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_REQ_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMKey)) & " "
        pDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_REQ_LOG_DET  " & vbCrLf & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMKey)) & " " & vbCrLf _
            & " AND AMEND_NO=" & VB6.Format(txtDSAmendNo.Text, "000") & " "
        pDBCn.Execute(SqlStr)

        DeleteDSDailyDetail = True
        Exit Function
DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteDSDailyDetail = False
    End Function

    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset = Nothing
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

                .Col = ColSchdQnty
                .Text = CStr(Val(CStr(mGrossQty)))

            Next I
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(.Text) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
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
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim SqlStr As String = ""
        Dim mItemCode As String = ""

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Please select the Customer.")
            Exit Sub
        End If

        If Trim(txtOurSONo.Text) = "" Then
            MsgInformation("Please select the Sales Order First.")
            Exit Sub
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                SqlStr = GetSearchSOItems("Y")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                SqlStr = GetSearchSOItems("N")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemName
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStoreLoc And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColStoreLoc
                    SqlStr = GetSearchSOItems("S", mItemCode)
                    ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColStoreLoc
                        .Text = Trim(AcName)
                    End If
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mSearchKey <> "" Then
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemDetail)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim xCustStoreLoc As String

        If eventArgs.NewRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        FormatSprdMain(eventArgs.row)
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If

                '        Case ColTotQty
                '            If CheckItemRate() = True Then
                '                MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
                '                FormatSprdMain -1
                '            End If
            Case ColStoreLoc
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo ErrPart
                SprdMain.Col = ColStoreLoc
                xCustStoreLoc = SprdMain.Text
                If xCustStoreLoc <> "" Then
                    'If GetValidCustomerStoreLoc(xICode, xCustStoreLoc) = False Then
                    '    'MsgInformation(xCustStoreLoc & " is a Invaild Store Loc for Item Code : " & xICode)
                    '    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStoreLoc)
                    '    Exit Sub
                    'End If
                    If GetValidCustomerStoreLocInPo(xICode, xCustStoreLoc) = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStoreLoc)
                        Exit Sub
                    End If
                End If


                If CheckDuplicateItem(SprdMain.Row) = True Then
                    'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCustStoreLoc)
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Public Function GetValidCustomerStoreLocInPo(ByRef pItemCode As String, ByRef pLocCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        ''           & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf _

        mSqlStr = "SELECT ID.ITEM_CODE " & vbCrLf _
            & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, INV_ITEM_MST INVMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & txtOurSONo.Text & "" & vbCrLf _
            & " AND IH.SO_STATUS='O' AND IH.SO_APPROVED='Y' "

        mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        mSqlStr = mSqlStr & vbCrLf & " AND ID.CUST_STORE_LOC='" & MainClass.AllowSingleQuote(pLocCode) & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidCustomerStoreLocInPo = True
        Else
            MsgInformation(pLocCode & " is not Valid Location for Item Code in Such Sale Order. " & pItemCode)
            GetValidCustomerStoreLocInPo = False
            Exit Function
        End If



        Exit Function
ErrPart:
        GetValidCustomerStoreLocInPo = False
    End Function
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        CheckQty = True
        Exit Function

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColSchdQnty
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MsgInformation("Please Enter the Qty.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColSchdQnty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM" & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mDSNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mDSNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))

        txtDSNo.Text = CStr(Val(mDSNo))

        txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    SprdView.Col = 1
    '    SprdView.Row = SprdView.ActiveRow
    '    txtDSNo.Text = SprdView.Text

    '    txtDSNo_Validating(txtDSNo, New System.ComponentModel.CancelEventArgs(False))
    '    CmdView_Click(CmdView, New System.EventArgs())
    'End Sub
    'Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
    '    If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    'End Sub

    Private Sub txtCustDSDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustDSDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustDSDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustDSDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If IsDate(txtCustDSDate.Text) = False Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCustDSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustDSNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustDSNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustDSNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustDSNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""


        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDSAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDSAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDSDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDSDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If IsDate(txtDSDate.Text) = False Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(txtDSDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOurSONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOurSONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOurSONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOurSONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOurSONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPOAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPOAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPOAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtScheduleDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScheduleDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Not IsDate(txtScheduleDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        '    If FYChk(txtScheduleDate.Text) = False Then
        '        Cancel = True
        '        Exit Sub
        '    End If

        If Val(VB6.Format(txtScheduleDate.Text, "YYYYMM")) < Val(VB6.Format(txtDSDate.Text, "YYYYMM")) Then
            MsgInformation("Schedule Date Cann't be Less Than Delivery Schedule Date")
            Cancel = True
        End If


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim I As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemName
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColCustPartNo
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemDetail)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If
            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSearchItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSearchItem.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        mSearchStartRow = 1
    End Sub
    Private Sub txtSearchItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSearchItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOurSONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOurSONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xAcctCode As String
        Dim mKey As Double

        If Val(txtOurSONo.Text) = 0 Then GoTo EventExitSub
        If Val(lblAutoSoNo.Text) = 0 Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "Supp_Cust_Name", "Supp_Cust_Code", "Fin_Supp_Cust_MSt", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("InValid Supplier Name.")
            GoTo EventExitSub
        End If

        mKey = Val(lblAutoSoNo.Text)

        SqlStr = " SELECT AUTO_KEY_SO, SO_DATE,CUST_PO_NO, CUST_PO_DATE , AMEND_NO, AMEND_DATE, AMEND_WEF_FROM " & vbCrLf & " FROM DSP_SALEORDER_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(CStr(mKey)) & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xAcctCode & "' AND SO_APPROVED='Y'"

        If CDate(txtScheduleDate.Text) < CDate(PubGSTApplicableDate) Then
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='N'"
        Else
            SqlStr = SqlStr & " AND ISGSTENABLE_PO='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtPODate.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_DATE").Value), "", RsTemp.Fields("CUST_PO_DATE").Value)
            txtPONo.Text = IIf(IsDbNull(RsTemp.Fields("CUST_PO_NO").Value), "", RsTemp.Fields("CUST_PO_NO").Value)
            txtPOAmendNo.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_NO").Value), "", RsTemp.Fields("AMEND_NO").Value)
            txtPOAmendDate.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_DATE").Value), "", RsTemp.Fields("AMEND_DATE").Value)
            txtWEF.Text = IIf(IsDbNull(RsTemp.Fields("AMEND_WEF_FROM").Value), "", RsTemp.Fields("AMEND_WEF_FROM").Value)
            txtOurSONo.Text = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_SO").Value), "", RsTemp.Fields("AUTO_KEY_SO").Value)
            lblAutoSodate.Text = IIf(IsDbNull(RsTemp.Fields("SO_DATE").Value), "", RsTemp.Fields("SO_DATE").Value)

        Else
            MsgBox("Invalid Customer SO NO.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
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
        If Not RsDSSMain.EOF Then

            lblMkey.Text = IIf(IsDbNull(RsDSSMain.Fields("AUTO_KEY_DELV").Value), "", RsDSSMain.Fields("AUTO_KEY_DELV").Value)
            txtDSNo.Text = IIf(IsDbNull(RsDSSMain.Fields("AUTO_KEY_DELV").Value), "", RsDSSMain.Fields("AUTO_KEY_DELV").Value)
            txtDSDate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("DELV_SCHLD_DATE").Value), "", RsDSSMain.Fields("DELV_SCHLD_DATE").Value), "DD/MM/YYYY")
            txtCustDSNo.Text = IIf(IsDbNull(RsDSSMain.Fields("CUST_DELV_NO").Value), 0, RsDSSMain.Fields("CUST_DELV_NO").Value)
            txtCustDSDate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("CUST_DELV_DATE").Value), "", RsDSSMain.Fields("CUST_DELV_DATE").Value), "DD/MM/YYYY")

            txtDSAmendNo.Text = IIf(IsDbNull(RsDSSMain.Fields("DELV_AMEND_NO").Value), 0, RsDSSMain.Fields("DELV_AMEND_NO").Value)
            txtDSAmendDate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("DELV_AMEND_DATE").Value), "", RsDSSMain.Fields("DELV_AMEND_DATE").Value), "DD/MM/YYYY")

            txtOurSONo.Text = IIf(IsDbNull(RsDSSMain.Fields("AUTO_KEY_SO").Value), 0, RsDSSMain.Fields("AUTO_KEY_SO").Value)

            txtPONo.Text = IIf(IsDbNull(RsDSSMain.Fields("CUST_SO_NO").Value), "", RsDSSMain.Fields("CUST_SO_NO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("CUST_SO_DATE").Value), "", RsDSSMain.Fields("CUST_SO_DATE").Value), "DD/MM/YYYY")
            txtPOAmendNo.Text = IIf(IsDbNull(RsDSSMain.Fields("SO_AMEND_NO").Value), "", RsDSSMain.Fields("SO_AMEND_NO").Value)
            txtPOAmendDate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("AMEND_DATE").Value), "", RsDSSMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")


            lblAutoSoNo.Text = Val(txtOurSONo.Text) & VB6.Format(Val(txtPOAmendNo.Text), "000")
            lblAutoSodate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("SO_DATE").Value), "", RsDSSMain.Fields("SO_DATE").Value), "DD/MM/YYYY")


            txtScheduleDate.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("SCHLD_DATE").Value), "", RsDSSMain.Fields("SCHLD_DATE").Value), "DD/MM/YYYY")
            cboStatus.SelectedIndex = IIf(RsDSSMain.Fields("SCHLD_STATUS").Value = "O", 0, 1)
            txtRemarks.Text = IIf(IsDbNull(RsDSSMain.Fields("REMARKS").Value), "", RsDSSMain.Fields("REMARKS").Value)

            chkApprovalBH.CheckState = IIf(RsDSSMain.Fields("APPROVAL_BH").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkApprovalPH.CheckState = IIf(RsDSSMain.Fields("APPROVAL_PH").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtWEF.Text = VB6.Format(IIf(IsDbNull(RsDSSMain.Fields("AMEND_WEF_DATE").Value), "", RsDSSMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

            If InStr(1, XRIGHT, "M") = 0 Then
                cmdAmendSchd.Enabled = False
                CmdPopFromFile.Enabled = False
            Else
                cmdAmendSchd.Enabled = IIf(RsDSSMain.Fields("SCHLD_STATUS").Value = "O", True, False)
            End If

            mAccountCode = IIf(IsDbNull(RsDSSMain.Fields("SUPP_CUST_CODE").Value), -1, RsDSSMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If
            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDbNull(RsDSSMain.Fields("SUPP_CUST_CODE").Value), "", RsDSSMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = False
            cmdsearch.Enabled = False
            txtOurSONo.Enabled = False
            cmdPoSearch.Enabled = False
            mAmendSchd = False
            Call ShowDetail1()
            Call ShowDSDailyDetail()

            If RsDSSMain.Fields("SCHLD_STATUS").Value = "C" Then
                cboStatus.Enabled = False
            End If
        End If

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True 'false
        txtDSNo.Enabled = True
        txtScheduleDate.Enabled = False
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
        MainClass.ButtonStatus(Me, XRIGHT, RsDSSMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mPreviousQty As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM DSP_DELV_SCHLD_REQ_DET " & vbCrLf & " WHERE " & vbCrLf & " AUTO_KEY_DELV=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsDSSDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode


                SprdMain.Col = ColCustPartNo
                mPartNo = ""
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_ITEM_NO", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                    mPartNo = MasterNo
                End If

                If mPartNo = "" Then
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPartNo = MasterNo
                    Else
                        mPartNo = ""
                    End If
                End If


                SprdMain.Text = Trim(mPartNo)

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColStoreLoc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("LOC_CODE").Value), "", .Fields("LOC_CODE").Value))

                If Val(txtDSAmendNo.Text) = 0 Then
                    mPreviousQty = 0
                Else
                    mPreviousQty = GetPreviousQty(mItemCode)
                End If

                SprdMain.Col = ColPreviousQty
                SprdMain.Text = CStr(Val(CStr(mPreviousQty)))

                SprdMain.Col = ColWeek1Qty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WEEK1_QTY").Value), 0, .Fields("WEEK1_QTY").Value)))

                SprdMain.Col = ColWeek2Qty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WEEK2_QTY").Value), 0, .Fields("WEEK2_QTY").Value)))

                SprdMain.Col = ColWeek3Qty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WEEK3_QTY").Value), 0, .Fields("WEEK3_QTY").Value)))

                SprdMain.Col = ColWeek4Qty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WEEK4_QTY").Value), 0, .Fields("WEEK4_QTY").Value)))

                SprdMain.Col = ColWeek5Qty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("WEEK5_QTY").Value), 0, .Fields("WEEK5_QTY").Value)))

                SprdMain.Col = ColSchdQnty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColAmendNo
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)))

                SprdMain.Col = ColAmendReason
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("AMEND_REASON").Value), "", .Fields("AMEND_REASON").Value))

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
    Private Function GetPreviousQty(ByRef mItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String
        Dim mWOPO As Boolean
        Dim mPurchaseUOM As String
        Dim mFactor As Double

        GetPreviousQty = 0
        SqlStr = " SELECT ID.ITEM_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_DELV_SCHLD_HDR IH, DSP_DELV_SCHLD_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & Trim(txtCode.Text) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtOurSONo.Text) & "" & vbCrLf _
            & " AND IH.SCHLD_DATE=TO_DATE('" & VB6.Format(txtScheduleDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousQty = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value))
        Else
            GetPreviousQty = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetPreviousQty = 0
    End Function
    Private Sub txtDSDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim xMkey As String = ""
        Dim mDSNo As Double
        Dim SqlStr As String = ""

        If Trim(txtDSNo.Text) = "" Then GoTo EventExitSub
        If Len(txtDSNo.Text) < 6 Then
            txtDSNo.Text = VB6.Format(Val(txtDSNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mDSNo = Val(txtDSNo.Text)

        If MODIFYMode = True And RsDSSMain.BOF = False Then xMkey = RsDSSMain.Fields("AUTO_KEY_DELV").Value

        SqlStr = "SELECT * FROM DSP_DELV_SCHLD_REQ_HDR " & " WHERE AUTO_KEY_DELV='" & MainClass.AllowSingleQuote(UCase(CStr(mDSNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.fields("FYEAR").value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDSSMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_DELV_SCHLD_REQ_HDR WHERE AUTO_KEY_DELV=" & Val(xMkey) & "" ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_DELV,LENGTH(AUTO_KEY_DELV)-5,4)=" & RsCompany.fields("FYEAR").value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDSSMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT B.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,B.ITEM_CODE "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"
        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))


        mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE='" & pItemCode & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        '    mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.text) = 0, 0, lblNetAmount.text)))
        '
        '    MainClass.AssignCRptFormulas Report1, "AmountInWord=""" & mAmountInword & """"
        '    MainClass.AssignCRptFormulas Report1, "NetAmount=""" & lblNetAmount.text & """"

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
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
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNO As String
        Dim Response As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)

        mTitle = "Sales Delivery Schedule / Confirmation"
        mRptFileName = "OrdercumDel.Rpt" ''"DS.rpt"
        If InsertIntoTempTable() = False Then GoTo ERR1

        SqlStr = " SELECT * FROM TEMP_DS WHERE USER_ID='" & MainClass.AllowSingleQuote(PubUserID) & "' ORDER BY ITEM_CODE, SERIAL_DATE"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

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


    Private Function DSExsistInCurrSchdMon(ByRef pSuppCustCode As String, ByRef pPONO As String, ByRef pSchdDate As String, ByRef pOrderType As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xDSNo As Double

        SqlStr = "SELECT AUTO_KEY_DELV " & vbCrLf & " FROM DSP_DELV_SCHLD_REQ_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSuppCustCode & "'" & vbCrLf & " AND AUTO_KEY_SO=" & Val(pPONO) & ""

        If pOrderType = "O" Then
            SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(pSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If ADDMode = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_DELV <> " & Val(txtDSNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xDSNo = RsTemp.Fields("AUTO_KEY_DELV").Value
            If pOrderType = "O" Then
                MsgInformation("Delivery Schedule (" & xDSNo & ") Already Made in this Month for Such Customer.")
            Else
                MsgInformation("Delivery Schedule (" & xDSNo & ") Already Made for Such Customer.")
            End If
            DSExsistInCurrSchdMon = True
        Else
            DSExsistInCurrSchdMon = False
        End If

        Exit Function
ErrPart:
        DSExsistInCurrSchdMon = True
    End Function

    Private Sub frmSalesDSRequisition_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        fraAccounts.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        FraTrn.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        fraTop1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        FraMovement.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    'Private Sub CalcTots()
    'On Error GoTo ERR1
    'Dim RsMisc As ADODB.Recordset = Nothing
    'Dim mGrossQty As Double
    '
    'Dim I As Long
    'Dim j As Long
    '
    '
    '    mGrossQty = 0
    '
    '    With SprdMain
    '        j = .MaxRows
    '        For I = 1 To j
    '            .Row = I
    '            mGrossQty = 0
    '
    '            .Col = ColWeek1Qty
    '            mGrossQty = mGrossQty + Val(.Text)
    '
    '            .Col = ColWeek2Qty
    '            mGrossQty = mGrossQty + Val(.Text)
    '
    '            .Col = ColWeek3Qty
    '            mGrossQty = mGrossQty + Val(.Text)
    '
    '            .Col = ColWeek4Qty
    '            mGrossQty = mGrossQty + Val(.Text)
    '
    '            .Col = ColWeek5Qty
    '            mGrossQty = mGrossQty + Val(.Text)
    '
    '            .Col = ColTotQty
    '            .Text = Val(mGrossQty)
    '
    '        Next I
    '    End With
    '
    '    Exit Sub
    'ERR1:
    '    ErrorMsg err.Description, err.Number, vbCritical
    '    ''Resume
    'End Sub

    Private Sub frmSalesDS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemDetail)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemDetail)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
