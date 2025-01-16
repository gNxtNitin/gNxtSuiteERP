Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTDSChallanCorrection
	Inherits System.Windows.Forms.Form
	Dim RSTDSChallan As ADODB.Recordset
	Dim ADDMode As Boolean
	Dim MODIFYMode As Boolean
	Dim XRIGHT As String
	'Dim PvtDBCn As ADODB.Connection
	Dim Shw As Boolean
	Dim FormActive As Boolean
	Dim xRefNo As Integer
	Dim SqlStr As String
	Private Const ColLocked As Short = 1
	Private Const ColVDate As Short = 2
	Private Const ColPartyName As Short = 3
	Private Const ColSection As Short = 4
	Private Const ColDeductAmt As Short = 5
	Private Const ColCessAmt As Short = 6
	Private Const ColSurcharge As Short = 7
	Private Const ColTDSAmount As Short = 8
	Private Const ColMKEY As Short = 9
	Private Const ColChallanMkey As Short = 10
	
	Private Const RowHeight As Short = 12
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub SetTextLength()
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetMaxLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: TextBox property TxtAccount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtAccount.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
		'UPGRADE_WARNING: TextBox property txtDateFrom.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDateFrom.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtDateTo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDateTo.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtBankName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBankName.Maxlength = RSTDSChallan.Fields("BANKNAME").DefinedSize
		'UPGRADE_WARNING: TextBox property txtBankCode.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBankCode.Maxlength = RSTDSChallan.Fields("BANKCODE").DefinedSize
		'UPGRADE_WARNING: TextBox property txtChallanDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtChallanDate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtChallanNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtChallanNo.Maxlength = RSTDSChallan.Fields("CHALLANNO").DefinedSize
		'UPGRADE_WARNING: TextBox property txtAmountPaid.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtAmountPaid.Maxlength = RSTDSChallan.Fields("AMOUNT").Precision
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetMaxLength. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: TextBox property txtSectionName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtSectionName.Maxlength = MainClass.SetMaxLength("NAME", "TDS_SECTION_MST", PubDBCn)
		'UPGRADE_WARNING: TextBox property txtChqNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtChqNo.Maxlength = RSTDSChallan.Fields("CHQ_NO").DefinedSize
		'UPGRADE_WARNING: TextBox property txtChqDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtChqDate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtTDSAmount.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtTDSAmount.Maxlength = RSTDSChallan.Fields("TDS_AMOUNT").Precision
		'UPGRADE_WARNING: TextBox property txtSurcharge.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtSurcharge.Maxlength = RSTDSChallan.Fields("SURCHARGE").Precision
		'UPGRADE_WARNING: TextBox property txtCess.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtCess.Maxlength = RSTDSChallan.Fields("EDU_CESS").Precision
		'UPGRADE_WARNING: TextBox property txtInterest.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtInterest.Maxlength = RSTDSChallan.Fields("INTEREST_AMOUNT").Precision
		'UPGRADE_WARNING: TextBox property txtOthers.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtOthers.Maxlength = RSTDSChallan.Fields("OTHER_AMOUNT").Precision
		
		
		Exit Sub
ERR1: 
		MsgBox(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		txtRefNo.Text = ""
		TxtAccount.Text = ""
		txtAmountPaid.Text = "0.00"
		txtBankName.Text = ""
		txtBankCode.Text = ""
		txtChallanNo.Text = ""
		txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		txtChallanDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		lblMKey.Text = ""
		
		txtSectionName.Text = ""
		txtChqNo.Text = ""
		txtChqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		txtTDSAmount.Text = "0.00"
		txtSurcharge.Text = "0.00"
		txtCess.Text = "0.00"
		txtInterest.Text = "0.00"
		txtOthers.Text = "0.00"
		
		txtRefNo.Enabled = True
		
		txtSectionName.Enabled = True
		cmdSection.Enabled = True
		TxtAccount.Enabled = True
		CmdSearch.Enabled = True
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdMain)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, True)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
		Dim MainClass_Renamed As Object
		On Error GoTo ModifyErr
		If CmdModify.Text = ConcmdmodifyCaption Then
			ADDMode = False
			MODIFYMode = True
			txtRefNo.Enabled = False
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, True)
		Else
			ADDMode = False
			MODIFYMode = False
			txtRefNo.Enabled = True
			Call Show1(False)
		End If
		Exit Sub
ModifyErr: 
		MsgBox(Err.Description)
	End Sub
	Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
		On Error GoTo AddErr
		If CmdAdd.Text = ConCmdAddCaption Then
			ADDMode = True
			MODIFYMode = False
			Clear1()
		Else
			ADDMode = False
			MODIFYMode = False
			txtRefNo.Enabled = True
			If RSTDSChallan.EOF = False Then RSTDSChallan.MoveFirst()
			Call Show1(False)
		End If
		Exit Sub
AddErr: 
		MsgBox(Err.Description)
		'Resume
	End Sub
	Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
		Me.Close()
	End Sub
	Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
		On Error GoTo DelErrPart
		If TxtAccount.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
		If Not RSTDSChallan.EOF Then
			If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
				If Delete1 = False Then GoTo DelErrPart
				If RSTDSChallan.EOF = True Then
					Clear1()
				Else
					Call Show1(False)
				End If
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
		
		If InsertIntoDelAudit(PubDBCn, "TDS_Challan_CORR", (lblMKey.Text), RSTDSChallan) = False Then GoTo DeleteErr
		If InsertIntoDeleteTrn(PubDBCn, "TDS_Challan_CORR", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr
		
		SqlStr = "Delete from TDS_Challan_CORR where MKey='" & lblMKey.Text & "' "
		PubDBCn.Execute(SqlStr)
		
		
		PubDBCn.CommitTrans()
		RSTDSChallan.Requery()
		Delete1 = True
		Exit Function
DeleteErr: 
		Delete1 = False
		PubDBCn.RollbackTrans()
		RSTDSChallan.Requery()
		MsgBox(Err.Description)
	End Function
	
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForChallan(Crystal.DestinationConstants.crptToWindow)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForChallan(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ReportForChallan(ByRef Mode As Crystal.DestinationConstants)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim All As Boolean
		Dim SqlStr As String
		Dim mTitle As String
		Dim mSubTitle As String
		Dim PrintStatus As Boolean
		Dim mReportFileName As String
		
		PubDBCn.Errors.Clear()
		
		PrintStatus = True
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		PubDBCn.Execute(SqlStr)
		
		SqlStr = ""
		
		'''''Select Record for print...
		
		SqlStr = ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.FetchFromTempData. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
		
		mTitle = "T.D.S. / T.C.S. Challan Correction"
		mSubTitle = ""
		
		mReportFileName = "TDSChallan.Rpt"
		
		Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		PubDBCn.Execute(SqlStr)
		
		Exit Sub
ERR1: 
		If Err.Number = 32755 Then
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		Else
			MsgInformation(Err.Description)
		End If
		'    Resume
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
		Dim MainClass_Renamed As Object
		Dim mAYEAR As String
		Dim mTaxType As String
		Dim mCompanyTan As String
		Dim mCompanyPhone As String
		Dim mCompanyPin As String
		Dim mPaymentCode As String
		Dim mTotalInWords As String
		Dim mAmountStr As String
		Dim CompanyAdd As String
		
		Dim mAmount As String
		Dim mCroreStr As String
		Dim mLacsStr As String
		Dim mThousandStr As String
		Dim mHundredStr As String
		Dim mTenStr As String
		Dim mUnitStr As String
		
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		CompanyAdd = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		CompanyAdd = CompanyAdd & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		CompanyAdd = CompanyAdd & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
		'    CompanyAdd = CompanyAdd & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & CompanyAdd & """")
		
		mAYEAR = Year(RsCompany.Fields("END_DATE").Value) & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AYear=""" & mAYEAR & """")
		
		mTaxType = "0020"
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TaxType=""" & mTaxType & """")
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCompanyTan = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyTan=""" & mCompanyTan & """")
		
		mCompanyPhone = "" ''IIf(IsNull(RsCompany!REGD_PHONE), "", RsCompany!REGD_PHONE)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCompanyPin = IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyPin=""" & mCompanyPin & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PaymentCode=""" & mPaymentCode & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtTDSAmount.Text, "0"))) & VB6.Format(txtTDSAmount.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "IncomeTax=""" & mAmountStr & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtSurcharge.Text, "0"))) & VB6.Format(txtSurcharge.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Surcharge=""" & mAmountStr & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtCess.Text, "0"))) & VB6.Format(txtCess.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "EduCess=""" & mAmountStr & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtInterest.Text, "0"))) & VB6.Format(txtInterest.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Interest=""" & mAmountStr & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtOthers.Text, "0"))) & VB6.Format(txtOthers.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Penalty=""" & mAmountStr & """")
		
		mAmountStr = New String(" ", 12 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Total=""" & mAmountStr & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(txtNetAmount.Text)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TotalInWords=""" & mTotalInWords & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "ChequeNo=""" & Trim(txtChqNo.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "ChequeDate=""" & Trim(txtChqDate.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "BankName=""" & Trim(txtBankName.Text) & """")
		
		mAmount = New String("0", 9 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
		mAmountStr = VB.Left(mAmount, 2)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mCroreStr = mTotalInWords
		
		mAmountStr = Mid(mAmount, 3, 2)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mLacsStr = mTotalInWords
		
		
		mAmountStr = Mid(mAmount, 5, 2)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mThousandStr = mTotalInWords
		
		mAmountStr = Mid(mAmount, 7, 1)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mHundredStr = mTotalInWords
		
		mAmountStr = Mid(mAmount, 8, 1)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mTenStr = mTotalInWords
		
		mAmountStr = VB.Right(mAmount, 1)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
		If Trim(mTotalInWords) = "" Then
			mTotalInWords = "Zero"
		Else
			mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
		End If
		mUnitStr = mTotalInWords
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CroreStr=""" & mCroreStr & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "LacsStr=""" & mLacsStr & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "ThousandStr=""" & mThousandStr & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "HundredStr=""" & mHundredStr & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TenStr=""" & mTenStr & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "UnitStr=""" & mUnitStr & """")
		
		' Report1.CopiesToPrinter = PrintCopies
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		Report1.Action = 1
	End Sub
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		Dim mFieldName As String
		'UPGRADE_WARNING: Untranslated statement in cmdsearch_Click. Please check source code.
	End Sub
	
	Private Sub cmdSection_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSection.Click
		Dim mFieldName As String
		'UPGRADE_WARNING: Untranslated statement in cmdSection_Click. Please check source code.
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
		Dim MainClass_Renamed As Object
		'If FieldsVarification = False Then Exit Sub
		If Trim(TxtAccount.Text) = "" Then
			MsgInformation("TDS Account Name is empty. Cannot Save")
			TxtAccount.Focus()
			Exit Sub
		End If
		
		If Trim(txtSectionName.Text) = "" Then
			MsgInformation("Section Cann't be Blank")
			txtSectionName.Focus()
			Exit Sub
		End If
		
		'UPGRADE_WARNING: Untranslated statement in cmdShow_Click. Please check source code.
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdMain, RowHeight)
		LedgInfo()
		FormatSprdMain()
		Call ReFormatSprdMain()
		SprdMain.Focus()
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetFocusToCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetFocusToCell(SprdMain, 1, 4)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdMain()
		Dim MainClass_Renamed As Object
		Dim cntCol As Integer
		With SprdMain
			.MaxCols = ColChallanMkey
			.set_RowHeight(0, RowHeight * 2)
			.set_ColWidth(0, 4.5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = ColLocked
			.CellType = SS_CELL_TYPE_CHECKBOX
			.TypeHAlign = SS_CELL_H_ALIGN_CENTER
			.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
			.set_ColWidth(ColLocked, 5.5)
			
			.Col = ColVDate
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(ColVDate, 8)
			
			.Col = ColPartyName
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(ColPartyName, 22)
			
			.Col = ColSection
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(ColSection, 6)
			
			
			For cntCol = ColDeductAmt To ColTDSAmount
				.Col = cntCol
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatMax = CDbl("999999999.99")
				.TypeFloatMin = CDbl("-999999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
				.TypeHAlign = SS_CELL_H_ALIGN_RIGHT
				.set_ColWidth(cntCol, 8)
			Next 
			
			.ColsFrozen = ColDeductAmt
			
			.Col = ColMKEY
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.ColHidden = True
			
			.Col = ColChallanMkey
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_RIGHT
			.ColHidden = True
			
			Call FillHeading()
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdMain, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdMain, 1, .MaxRows, 2, .MaxCols)
			SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
			SprdMain.DAutoCellTypes = True
			SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
			SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
		ViewGrid()
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ViewGrid()
		Dim MainClass_Renamed As Object
		Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
		If CmdView.Text = ConCmdGridViewCaption Then
			CmdView.Text = ConCmdViewCaption
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ClearGrid(SprdView)
			AssignGrid(True)
			'        ADataGrid.Refresh
			FormatSprdView()
			'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			SprdView.CtlRefresh()
			SprdView.Focus()
			SprdView.BringToFront()
		Else
			CmdView.Text = ConCmdGridViewCaption
			FraView.BringToFront()
		End If
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, True)
		Me.Cursor = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTDSChallanCorrection_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.DoFunctionKey. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.DoFunctionKey(Me, KeyCode)
	End Sub
	
	'UPGRADE_WARNING: Event OptSelection.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = OptSelection.GetIndex(eventSender)
			Dim cntRow As Integer
			
			For cntRow = 1 To SprdMain.MaxRows
				SprdMain.Row = cntRow
				SprdMain.Col = ColLocked
				SprdMain.Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
			Next 
			CalcChallanAmount()
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
		Dim MainClass_Renamed As Object
		If Row = 0 Then Exit Sub
		SprdMain.Row = Row
		SprdMain.Col = ColLocked
		SprdMain.Value = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
		CalcChallanAmount()
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
		SprdView.Col = 1
		SprdView.Row = SprdView.ActiveRow
		txtRefNo.Text = Trim(SprdView.Text)
		
		txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
		CmdView_Click(CmdView, New System.EventArgs())
		
	End Sub
	
	Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
		If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtAccount.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAccount.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Form event frmTDSChallanCorrection.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmTDSChallanCorrection_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		If FormActive = True Then Exit Sub
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet("Select * From TDS_Challan_CORR Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
		SqlStr = ""
		SetTextLength()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call AssignGrid(False)
		Clear1()
		If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
		FormActive = True
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ERR1: 
		MsgBox(Err.Description)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTDSChallanCorrection_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		''Set PvtDBCn = New ADODB.Connection
		''PvtDBCn.Open StrConn
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.STRMenuRight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RightsToButton. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.RightsToButton(Me, XRIGHT)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetControlsColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetControlsColor(Me)
		ADDMode = False
		MODIFYMode = False
		Me.Left = 0
		Me.Top = 0
		Me.Height = VB6.TwipsToPixelsY(7245)
		Me.Width = VB6.TwipsToPixelsX(9945)
		FormatSprdMain()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	Private Sub frmTDSChallanCorrection_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		On Error Resume Next
		'UPGRADE_NOTE: Object RSTDSChallan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		RSTDSChallan = Nothing
		'UPGRADE_NOTE: Object frmTDSChallan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		frmTDSChallan = Nothing
		'    PubDBCn.Cancel
		'    PvtDBCn.Close
		'    Set PvtDBCn = Nothing
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Show1(ByRef mIsAddMode As Boolean)
		Dim MainClass_Renamed As Object
		On Error GoTo ShowErrPart
		Dim mSection As String
		
		Shw = True
		If Not RSTDSChallan.EOF Then
			txtRefNo.Enabled = True
			With RSTDSChallan
				'UPGRADE_WARNING: Untranslated statement in Show1. Please check source code.
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtRefNo.Text = IIf(IsDbNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtDateFrom.Text = VB6.Format(IIf(IsDbNull(.Fields("FROMDATE").Value), "", .Fields("FROMDATE").Value), "DD/MM/YYYY")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtDateTo.Text = VB6.Format(IIf(IsDbNull(.Fields("TODATE").Value), "", .Fields("TODATE").Value), "DD/MM/YYYY")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtBankName.Text = IIf(IsDbNull(.Fields("BANKNAME").Value), "", .Fields("BANKNAME").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtBankCode.Text = IIf(IsDbNull(.Fields("BANKCODE").Value), "", .Fields("BANKCODE").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtChallanDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHALLANDATE").Value), "", .Fields("CHALLANDATE").Value), "DD/MM/YYYY")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtChallanNo.Text = IIf(IsDbNull(.Fields("CHALLANNO").Value), "", .Fields("CHALLANNO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtAmountPaid.Text = VB6.Format(IIf(IsDbNull(.Fields("Amount").Value), 0, .Fields("Amount").Value), "0.00")
				lblMKey.Text = RSTDSChallan.Fields("mKey").Value
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mSection = IIf(IsDbNull(.Fields("SECTIONCODE").Value), "", .Fields("SECTIONCODE").Value)
				'UPGRADE_WARNING: Untranslated statement in Show1. Please check source code.
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtChqNo.Text = IIf(IsDbNull(.Fields("CHQ_NO").Value), "", .Fields("CHQ_NO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtChqDate.Text = VB6.Format(IIf(IsDbNull(.Fields("CHQ_DATE").Value), "", .Fields("CHQ_DATE").Value), "DD/MM/YYYY")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtTDSAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TDS_AMOUNT").Value), 0, .Fields("TDS_AMOUNT").Value), "0.00")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtSurcharge.Text = VB6.Format(IIf(IsDbNull(.Fields("SURCHARGE").Value), 0, .Fields("SURCHARGE").Value), "0.00")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtCess.Text = VB6.Format(IIf(IsDbNull(.Fields("EDU_CESS").Value), 0, .Fields("EDU_CESS").Value), "0.00")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtInterest.Text = VB6.Format(IIf(IsDbNull(.Fields("INTEREST_AMOUNT").Value), 0, .Fields("INTEREST_AMOUNT").Value), "0.00")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				txtOthers.Text = VB6.Format(IIf(IsDbNull(.Fields("OTHER_AMOUNT").Value), 0, .Fields("OTHER_AMOUNT").Value), "0.00")
				
				xRefNo = RSTDSChallan.Fields("REFNO").Value
				txtSectionName.Enabled = False
				cmdSection.Enabled = False
				
				TxtAccount.Enabled = False
				CmdSearch.Enabled = False
				
			End With
			Call cmdShow_Click(cmdShow, New System.EventArgs())
		End If
		Shw = False
		ADDMode = mIsAddMode
		MODIFYMode = False
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RSTDSChallan, ADDMode, MODIFYMode, True)
		Exit Sub
ShowErrPart: 
		MsgBox(Err.Description)
	End Sub
	Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
		On Error GoTo ErrorHandler
		If FieldsVarification = False Then
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		If Update1 = True Then
			txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
			If CmdAdd.Enabled = True Then CmdAdd.Focus()
		Else
			MsgInformation("Record not saved")
		End If
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrorHandler: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgBox(Err.Description)
	End Sub
	Private Function Update1() As Boolean
		On Error GoTo UpdateError
		Dim mTDSCode As String
		Dim mRefNo As Integer
		Dim pMkey As String
		Dim mSectionCode As Double
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		SqlStr = ""
		'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
		
		'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
		
		
		If ADDMode = True Then
			mRefNo = Val(txtRefNo.Text)
			pMkey = (IIf(PubHO = "Y", 50, 0) + RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mRefNo
			
			'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
			
		Else
			'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
			
			pMkey = lblMKey.Text
		End If
		
UpdatePart: 
		PubDBCn.Execute(SqlStr)
		
		PubDBCn.CommitTrans()
		RSTDSChallan.Requery()
		Update1 = True
		Exit Function
UpdateError: 
		Update1 = False
		PubDBCn.RollbackTrans()
		RSTDSChallan.Requery()
		If Err.Number = -2147467259 Then
			MsgBox("Can't Modify Transaction Exists Against this Code")
			Exit Function
		End If
		MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
		''Resume
		PubDBCn.Errors.Clear()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Function
	Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
		cmdsearch_Click(cmdsearch, New System.EventArgs())
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAccount.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
	End Sub
	Private Function FieldsVarification() As Boolean
		On Error GoTo VarificationErr
		FieldsVarification = True
		If Trim(TxtAccount.Text) = "" Then
			MsgInformation("TDS Account Name is empty. Cannot Save")
			TxtAccount.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Trim(txtBankName.Text) = "" Then
			MsgInformation("Bank Name is empty. Cannot Save")
			txtBankName.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Trim(txtBankCode.Text) = "" Then
			MsgInformation("Bank Code is empty. Cannot Save")
			txtBankCode.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Len(txtBankCode.Text) <> 7 Then
			MsgInformation("Invalid Bank Code. Cannot Save")
			txtBankCode.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Trim(txtChallanDate.Text) = "" Then
			MsgInformation("Challan Date is empty. Cannot Save")
			txtChallanDate.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Not IsDate(txtChallanDate.Text) Then
			MsgInformation("Invalid Challan Date. Cannot Save")
			txtChallanDate.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Trim(txtChallanNo.Text) = "" Then
			MsgInformation("Challan No is empty. Cannot Save")
			txtChallanNo.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Val(txtNetAmount.Text) = 0 Then
			MsgInformation("Net Amount is zero. Cannot Save")
			SprdMain.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Trim(txtSectionName.Text) = "" Then
			MsgInformation("Section Cann't be Blank")
			txtSectionName.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		'UPGRADE_WARNING: Untranslated statement in FieldsVarification. Please check source code.
		
		If ADDMode = False And MODIFYMode = False Then
			MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
			FieldsVarification = False
		End If
		''If MODIFYMode = True And (RSTDSChallan.EOF=true Or RSTDSChallan.EOF = True) Then Exit Function
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Function
VarificationErr: 
		FieldsVarification = False
		MsgInformation(Err.Description)
	End Function
	
	Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAccount.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		On Error GoTo ErrPart
		
		If Trim(TxtAccount.Text) = "" Then GoTo EventExitSub
		
		'UPGRADE_WARNING: Untranslated statement in txtAccount_Validate. Please check source code.
		GoTo EventExitSub
ErrPart: 
		MsgBox(Err.Description)
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtAmountPaid.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtAmountPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmountPaid.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtAmountPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountPaid.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtBankCode.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtBankCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankCode.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtBankCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankCode.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtBankCode)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtBankName.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtCess.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtCess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCess.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtCess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCess.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtCess_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCess.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Call CalcTDSAmount()
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtChallanDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(txtChallanDate.Text) Then
			MsgBox("Invalid Challan Date", MsgBoxStyle.Information)
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtChallanNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtChallanNo)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtChqDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(txtChqDate.Text) Then
			MsgBox("Invalid Cheque / DD Date", MsgBoxStyle.Information)
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtChqNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtDatefrom.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDatefrom.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtDateTo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtInterest.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtInterest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInterest.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtInterest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInterest.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtInterest_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInterest.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Call CalcTDSAmount()
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtNetAmount.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtNetAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetAmount.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtNetAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmount.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtNetAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNetAmount.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Call CalcTDSAmount()
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtOthers.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Call CalcTDSAmount()
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtRefNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		
		If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
		If MODIFYMode = True And RSTDSChallan.EOF = False Then xRefNo = RSTDSChallan.Fields("REFNO").Value
		
		SqlStr = ""
		SqlStr = "Select * from  TDS_Challan_CORR Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
		If RSTDSChallan.EOF = False Then
			ADDMode = False
			MODIFYMode = False
			Call Show1(False)
		Else
			SqlStr = "Select * from  TDS_Challan Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
			If RSTDSChallan.EOF = False Then
				ADDMode = True
				MODIFYMode = False
				Call Show1(True)
			Else
				
				If ADDMode = False And MODIFYMode = False Then
					MsgBox("Click Add for New", MsgBoxStyle.Information)
					Cancel = True
				ElseIf MODIFYMode = True Then 
					'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					MainClass.UOpenRecordSet("Select * From TDS_Challan_CORR Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RefNo=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSTDSChallan, ADODB.LockTypeEnum.adLockReadOnly)
				End If
			End If
		End If
		
		GoTo EventExitSub
ERR1: 
		MsgInformation(Err.Description)
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub LedgInfo()
		Dim MainClass_Renamed As Object
		On Error GoTo LedgError
		Dim SqlStr As String
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		SqlStr = MakeSQL()
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, AData1, StrConn, "Y")
		
		CalcChallanAmount()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
LedgError: 
		MsgInformation(Err.Description)
		
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Function MakeSQL() As String
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		Dim mAccountCode As String
		Dim mChallanNo As String
		Dim mSectionCode As Double
		
		'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.
		
		'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.
		
		mChallanNo = lblMKey.Text
		
		SqlStr = " Select DECODE(CHALLANMKEY,NULL,'0','1') AS LOCKED ,TO_CHAR(Vdate,'DD/MM/YYYY') AS VDate, " & vbCrLf & " DECODE(PARTYNAME,'-1','',PARTYNAME) AS PartyName, " & vbCrLf & " TDSSection.Name As SectionName,  " & vbCrLf & " TO_CHAR(TDSAMOUNT) As Amount, "
		
		
		If RsCompany.Fields("FYEAR").Value < 2004 Then
			SqlStr = SqlStr & vbCrLf & " '0.00' As CessAmount, " & vbCrLf & " TO_CHAR((TDSAMOUNT*100/110.0)*.10) As SurAmount, " & vbCrLf & " TO_CHAR(TDSAMOUNT - ((TDSAMOUNT*100*.100/110.0))) As TDSAmount,"
		Else
			SqlStr = SqlStr & vbCrLf & " TO_CHAR((TDSAMOUNT*100/112.2)*.022) As CessAmount, " & vbCrLf & " TO_CHAR((TDSAMOUNT*100/112.2)*.100) As SurAmount, " & vbCrLf & " TO_CHAR(TDSAMOUNT - ((TDSAMOUNT*100*.022/112.2)+ (TDSAMOUNT*100*.100/112.2))) As TDSAmount,"
		End If
		
		SqlStr = SqlStr & vbCrLf & " TDSTRN.Mkey,TDSTRN.ChallanMKey " & vbCrLf & " FROM TDS_TRN TDSTRN, TDS_Section_MST TDSSection, FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE " & vbCrLf & " TDSTRN.COMPANY_CODE = ACM.COMPANY_CODE " & vbCrLf & " AND TDSTRN.COMPANY_CODE = TDSSection.COMPANY_CODE(+) " & vbCrLf & " AND TDSTRN.AccountCode = ACM.SUPP_CUST_CODE " & vbCrLf & " AND TDSTRN.SectionCode = TDSSection.Code(+) " & vbCrLf & " AND TDSTRN.Vdate>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' " & vbCrLf & " AND TDSTRN.Vdate<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' " & vbCrLf & " AND TDSTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TDSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TDSTRN.AccountCode = '" & mAccountCode & "'"
		
		If mChallanNo = "" Then
			SqlStr = SqlStr & vbCrLf & " AND (TDSTRN.CHALLANMKEY='' OR TDSTRN.CHALLANMKEY IS NULL)"
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = SqlStr & vbCrLf & " AND (TDSTRN.CHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanNo) & "' OR TDSTRN.CHALLANMKEY='' OR TDSTRN.CHALLANMKEY IS NULL)"
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"
		
		SqlStr = SqlStr & vbCrLf & " AND TDSTRN.SECTIONCODE=" & mSectionCode & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY TDSTRN.Vdate,TDSTRN.Vno,TDSTRN.BOOKTYPE,TDSTRN.BOOKSUBTYPE,TDSTRN.SUBROWNO "
		
		MakeSQL = SqlStr
		Exit Function
ERR1: 
		MsgInformation(Err.Description)
		MakeSQL = ""
	End Function
	
	Private Sub FillHeading()
		
		
		With SprdMain
			.Row = 0
			.Col = ColLocked
			.Text = "Update"
			
			.Col = ColVDate
			.Text = "Date"
			
			.Col = ColPartyName
			.Text = "Party Name"
			
			.Col = ColSection
			.Text = "Section Name"
			
			.Col = ColDeductAmt
			.Text = "Amount"
			
			.Col = ColCessAmt
			.Text = "Cess"
			
			.Col = ColSurcharge
			.Text = "Surcharge"
			
			.Col = ColTDSAmount
			.Text = "TDS Amount"
			
			.Col = ColMKEY
			.Text = "MKey"
			
			.Col = ColChallanMkey
			.Text = "ChallanMkey"
			
		End With
		
	End Sub
	
	Private Sub CalcChallanAmount()
		On Error GoTo ErrPart
		Dim cntRow As Integer
		Dim mNetAmount As Double
		Dim mCESSAmount As Double
		Dim mSURAmount As Double
		Dim mTDSAmount As Double
		
		mNetAmount = 0
		mCESSAmount = 0
		mSURAmount = 0
		mTDSAmount = 0
		With SprdMain
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = ColLocked
				
				If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then GoTo NextRow
				
				.Col = ColDeductAmt
				mNetAmount = mNetAmount + Val(.Text)
				
				.Col = ColCessAmt
				mCESSAmount = mCESSAmount + Val(.Text)
				
				.Col = ColSurcharge
				mSURAmount = mSURAmount + Val(.Text)
				
				.Col = ColTDSAmount
				mTDSAmount = mTDSAmount + Val(.Text)
				
				
NextRow: 
			Next 
		End With
		mCESSAmount = System.Math.Round(mCESSAmount, 0)
		mSURAmount = System.Math.Round(mSURAmount, 0)
		
		'    mCESSAmount = mCESSAmount
		'    mSURAmount = mSURAmount
		mTDSAmount = mNetAmount - (mCESSAmount + mSURAmount)
		
		txtAmountPaid.Text = VB6.Format(mNetAmount, "0.00")
		txtCess.Text = VB6.Format(mCESSAmount, "0.00")
		txtSurcharge.Text = VB6.Format(mSURAmount, "0.00")
		txtTDSAmount.Text = VB6.Format(mTDSAmount, "0.00")
		
		Call CalcTDSAmount()
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
	End Sub
	
	Private Sub CalcTDSAmount()
		On Error GoTo ErrPart
		
		txtTDSAmount.Text = CStr(Val(txtAmountPaid.Text) - (System.Math.Round(Val(txtSurcharge.Text), 0) + System.Math.Round(Val(txtCess.Text), 0)))
		txtTDSAmount.Text = VB6.Format(txtTDSAmount.Text, "0.00")
		
		txtNetAmount.Text = CStr(Val(txtAmountPaid.Text) + Val(txtInterest.Text) + Val(txtOthers.Text))
		txtNetAmount.Text = VB6.Format(txtNetAmount.Text, "0.00")
		
		
		'    txtTDSAmount.Text = Val(txtAmountPaid.Text) - (Val(txtSurcharge.Text) + Val(txtCess.Text))
		'    txtTDSAmount.Text = Format(txtTDSAmount.Text, "0.00")
		'
		'    txtNetAmount.Text = Val(txtAmountPaid.Text) + Val(txtInterest.Text) + Val(txtOthers.Text)
		'    txtNetAmount.Text = Format(txtNetAmount.Text, "0.00")
		
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
	End Sub
	Private Sub ReFormatSprdMain()
		On Error GoTo ErrPart
		Dim cntRow As Integer
		Dim mChallanNo As String
		
		With SprdMain
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = ColChallanMkey
				mChallanNo = Trim(.Text)
				
				.Col = ColLocked
				If mChallanNo = "" Then
					.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
				Else
					.Value = CStr(System.Windows.Forms.CheckState.Checked)
				End If
			Next 
		End With
		
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub AssignGrid(ByRef mRefresh As Boolean)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		SqlStr = ""
		
		SqlStr = " Select TO_CHAR(REFNO,'00000') AS REFNO,TO_CHAR(FROMDATE,'DD/MM/YYYY') AS FROMDATE, " & vbCrLf & " TO_CHAR(TODATE,'DD/MM/YYYY') AS TODATE," & vbCrLf & " BANKNAME, CHALLANNO, " & vbCrLf & " TO_CHAR(CHALLANDATE,'DD/MM/YYYY') AS ChallanDate, " & vbCrLf & " TO_CHAR(AMOUNT) As Amount " & vbCrLf & " FROM TDS_Challan_CORR TDSChallan" & vbCrLf & " WHERE " & vbCrLf & " TDSChallan.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TDSChallan.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY TDSChallan.REFNO"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, ADataGrid, StrConn, IIf(mRefresh = True, "Y", "N"))
		FormatSprdView()
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView()
		Dim MainClass_Renamed As Object
		With SprdView
			.Row = -1
			.set_RowHeight(0, 300)
			.set_ColWidth(0, 0)
			.set_ColWidth(1, 1000)
			.set_ColWidth(2, 1000)
			.set_ColWidth(3, 1000)
			.set_ColWidth(4, 2500)
			.set_ColWidth(5, 1500)
			.set_ColWidth(6, 1500)
			.set_ColWidth(7, 1000)
			.ColsFrozen = 1
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView, -1)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.CellColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtSectionName.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtSectionName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	Private Sub txtSectionName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.DoubleClick
		cmdSection_Click(cmdSection, New System.EventArgs())
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtSectionName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSectionName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtSectionName)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	Private Sub txtSectionName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSectionName.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSection_Click(cmdSection, New System.EventArgs())
	End Sub
	Private Sub txtSectionName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSectionName.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		On Error GoTo ErrPart
		
		If Trim(txtSectionName.Text) = "" Then GoTo EventExitSub
		
		'UPGRADE_WARNING: Untranslated statement in txtSectionName_Validate. Please check source code.
		
		GoTo EventExitSub
ErrPart: 
		MsgBox(Err.Description)
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtSurcharge.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurcharge.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurcharge.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	
	Private Sub txtSurcharge_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSurcharge.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		Call CalcTDSAmount()
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtTDSAmount.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class