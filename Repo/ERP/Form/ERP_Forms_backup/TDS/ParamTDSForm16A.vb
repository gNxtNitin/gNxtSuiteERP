Option Strict Off
Option Explicit On
Friend Class frmParamForm16A
	Inherits System.Windows.Forms.Form
	'Dim PvtDBCn As ADODB.Connection
	
	'UPGRADE_WARNING: Event chkAllCerti.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkAllCerti_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCerti.CheckStateChanged
		txtCertificateNo.Enabled = IIf(chkAllCerti.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		CmdSearchCNo.Enabled = IIf(chkAllCerti.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
	End Sub
	
	'UPGRADE_WARNING: Event ChkAllParty.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub ChkAllParty_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkAllParty.CheckStateChanged
		txtCustomer.Enabled = IIf(ChkAllParty.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		cmdSearch.Enabled = IIf(ChkAllParty.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
	End Sub
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		Call ReportOnSection(Crystal.DestinationConstants.crptToWindow)
	End Sub
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		Call ReportOnSection(Crystal.DestinationConstants.crptToPrinter)
	End Sub
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		'UPGRADE_WARNING: Untranslated statement in cmdsearch_Click. Please check source code.
	End Sub
	
	Private Sub CmdSearchCNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCNo.Click
		Dim pSectionCode As Integer
		
		'UPGRADE_WARNING: Untranslated statement in CmdSearchCNo_Click. Please check source code.
		'UPGRADE_WARNING: Untranslated statement in CmdSearchCNo_Click. Please check source code.
	End Sub
	
	
	'UPGRADE_WARNING: Form event frmParamForm16A.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmParamForm16A_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		Me.Text = "TDS FORM 16A"
	End Sub
	Private Sub frmParamForm16A_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		On Error GoTo ERR1
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		''Set PvtDBCn = New ADODB.Connection
		''PvtDBCn.Open StrConn
		Me.Width = VB6.TwipsToPixelsX(6015)
		Me.Height = VB6.TwipsToPixelsY(4620)
		Me.Top = 0
		Me.Left = 0
		txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
		txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		
		txtCertificateNo.Enabled = False
		CmdSearchCNo.Enabled = False
		txtCustomer.Enabled = False
		cmdSearch.Enabled = False
		
		Call FillLst()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ERR1: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FillLst()
		Dim MainClass_Renamed As Object
		On Error GoTo FillERR
		Dim RsSection As ADODB.Recordset
		Dim SqlStr As String
		LstSection.Items.Clear()
		SqlStr = "SELECT NAME FROM TDS_SECTION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order By NAME"
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSection, ADODB.LockTypeEnum.adLockReadOnly)
		If RsSection.EOF = False Then
			Do While Not RsSection.EOF
				LstSection.Items.Add(RsSection.Fields("Name").Value)
				RsSection.MoveNext()
			Loop 
		End If
		LstSection.SelectedIndex = 0
		Exit Sub
FillERR: 
		MsgBox(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ShowReport(ByRef ReportSQL As String, ByRef Mode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pTDSAmount As Double, Optional ByRef mWithZeroBal As String = "", Optional ByRef mQTRPrint As String = "")
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim mTDSAmtInWord As String
		Dim mCAddress As String
		Dim mAckNo1QTR As String
		Dim mAckNo2QTR As String
		Dim mAckNo3QTR As String
		Dim mAckNo4QTR As String
		Dim xSqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim mCITName As String
		Dim mCITAddress As String
		Dim mCITCity As String
		Dim mCITPincode As String
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RupeesConversion. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTDSAmtInWord = MainClass.RupeesConversion(pTDSAmount)
		
		mTDSAmtInWord = "Certified that a sum of " & mTDSAmtInWord
		SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)
		
		'    MainClass.AssignCRptFormulas Report1, "CompanyName=""" & RsCompany!Company_Name & """"
		'    MainClass.AssignCRptFormulas Report1, "CompanyAddress=""" & RsCompany!COMPANY_ADDR & RsCompany!COMPANY_CITY & RsCompany!COMPANY_STATE & RsCompany!COMPANY_PIN & """"
		
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCAddress = mCAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCAddress = mCAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCAddress = mCAddress & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value), "", "-" & RsCompany.Fields("REGD_STATE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mCAddress = mCAddress & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", "-" & RsCompany.Fields("REGD_PIN").Value)
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CAddress=""" & mCAddress & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyTDSCircle=""" & RsCompany.Fields("TDSCIRCLE").Value & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyTDSAC=""" & RsCompany.Fields("TDSACNO").Value & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyPANNO=""" & RsCompany.Fields("PAN_NO").Value & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FromDate=""" & txtDateFrom.Text & """") ''RsCompany!Start_Date
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "ToDate=""" & txtDateTo.Text & """") ''RsCompany!END_DATE
		
		'    MainClass.AssignCRptFormulas Report1, "WordinRupees=""" & mTDSAmtInWord & """"
		
		If mQTRPrint = "Y" Then
			xSqlStr = " SELECT * FROM TDS_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
			
			If RsTemp.EOF = False Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mAckNo1QTR = IIf(IsDbNull(RsTemp.Fields("I_QTR_NO").Value), "", RsTemp.Fields("I_QTR_NO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mAckNo2QTR = IIf(IsDbNull(RsTemp.Fields("II_QTR_NO").Value), "", RsTemp.Fields("II_QTR_NO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mAckNo3QTR = IIf(IsDbNull(RsTemp.Fields("III_QTR_NO").Value), "", RsTemp.Fields("III_QTR_NO").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mAckNo4QTR = IIf(IsDbNull(RsTemp.Fields("IV_QTR_NO").Value), "", RsTemp.Fields("IV_QTR_NO").Value)
			End If
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AckNo1QTR='" & mAckNo1QTR & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AckNo2QTR='" & mAckNo2QTR & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AckNo3QTR='" & mAckNo3QTR & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AckNo4QTR='" & mAckNo4QTR & "'")
		End If
		
		If RsCompany.Fields("FYEAR").Value >= 2010 Then
			xSqlStr = " SELECT CIRCLE_NAME,CIRCLE_ADDRESS,CIRCLE_CITY,CIRCLE_PINCODE " & vbCrLf & " FROM FIN_PRINT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
			
			If RsTemp.EOF = False Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCITName = IIf(IsDbNull(RsTemp.Fields("CIRCLE_NAME").Value), "", RsTemp.Fields("CIRCLE_NAME").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCITAddress = IIf(IsDbNull(RsTemp.Fields("CIRCLE_ADDRESS").Value), "", RsTemp.Fields("CIRCLE_ADDRESS").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCITCity = IIf(IsDbNull(RsTemp.Fields("CIRCLE_CITY").Value), "", RsTemp.Fields("CIRCLE_CITY").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCITPincode = IIf(IsDbNull(RsTemp.Fields("CIRCLE_PINCODE").Value), "", RsTemp.Fields("CIRCLE_PINCODE").Value)
			End If
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "mCITName='" & mCITName & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "mCITAddress='" & mCITAddress & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "mCITCity='" & mCITCity & "'")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "mCITPincode='" & mCITPincode & "'")
			
		End If
		
		Report1.SQLQuery = ReportSQL
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Report1.Action = 1
		Report1.PageZoom((100))
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If MainClass.ClearCRptFormulas(Report1) = False Then GoTo ErrPart
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ReportOnSection(ByRef Mode As Crystal.DestinationConstants)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim mTitle As String
		Dim mSubTitle As String
		Dim SqlStr As String
		Dim mTDSAmount As Double
		
		
		If FieldVarification = False Then
			Exit Sub
		End If
		
		
		mTitle = ""
		mSubTitle = ""
		mTDSAmount = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearCRptFormulas(Report1)
		Report1.Reset()
		
		SqlStr = MakeSQL()
		
		'    mTDSAmount = Get_Sum_TDSAmount(False)
		
		mSubTitle = RsCompany.Fields("START_DATE").Value & " To " & RsCompany.Fields("END_DATE").Value
		mTitle = ""
		
		If RsCompany.Fields("FYEAR").Value >= 2010 Then
			Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SectionPrint2010.Rpt"
			Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mTDSAmount, "", "Y")
		Else
			Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SectionPrint.Rpt"
			Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mTDSAmount, "", "Y")
			
			Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SectionAnnxPrint.Rpt"
			Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mTDSAmount)
		End If
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		'    SqlStr = "UPDATE TDS_TRN SET NewCertificate='N' WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND NewCertificate='Y'"
		'    PubDBCn.Execute SqlStr
		PubDBCn.CommitTrans()
		
		Exit Sub
ERR1: 
		'    Resume
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgBox(Err.Description)
		PubDBCn.RollbackTrans()
	End Sub
	
	
	Private Function FieldVarification() As Boolean
		
		On Error GoTo err_Renamed
		FieldVarification = True
		If chkAllCerti.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			If txtCertificateNo.Text = "" Then
				MsgInformation("Certificate No is empty.")
				txtCertificateNo.Focus()
				FieldVarification = False
				Exit Function
			End If
		End If
		
		If ChkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			If txtCustomer.Text = "" Then
				MsgInformation("Party Name is empty.")
				txtCustomer.Focus()
				FieldVarification = False
				Exit Function
			End If
			
			'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.
		End If
		
		'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.
		If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then FieldVarification = False : txtDateFrom.Focus()
		'UPGRADE_WARNING: Untranslated statement in FieldVarification. Please check source code.
		If FYChk(CStr(CDate(txtDateTo.Text))) = False Then FieldVarification = False : txtDateTo.Focus()
		
		Exit Function
err_Renamed: 
		MsgBox(Err.Description)
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function MakeSQL() As String
		Dim MainClass_Renamed As Object
		On Error GoTo SqlERR
		Dim mSection As String
		Dim pSqlStr As String
		
		pSqlStr = " SELECT TDSTRN.MKEY, " & vbCrLf & " TDSTRN.VDATE, FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS PARTYNAME, TDSTRN.AMOUNTPAID, TDSTRN.TDSRATE, " & vbCrLf & " TDSTRN.TDSAMOUNT, TDSTRN.CHALLANDATE, TDSTRN.CHALLANNO, " & vbCrLf & " TDSTRN.BANKNAME, TDSTRN.PANNO, " & vbCrLf & " TDSSECTION.NAME, TDSSECTION.NATURE, " & vbCrLf & " TDSCHALLAN.FROMDATE, TDSCHALLAN.TODATE, TDSTRN.SECTIONCODE, " & vbCrLf & " Place, PrintDate, Authorized, Authorized_FNAME, Authorized_Desig,FIN_SUPP_CUST_MST.SUPP_CUST_ADDR,ASSESSMENT_YEAR, PERIOD_FROM, PERIOD_TO, QTR_NAME " & vbCrLf & " FROM  TDS_TRN TDSTRN ,  TDS_CHALLAN TDSCHALLAN, TDS_SECTION_MST TDSSECTION,FIN_SUPP_CUST_MST " & vbCrLf & " WHERE TDSTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSTRN.COMPANY_CODE = TDSCHALLAN.COMPANY_CODE " & vbCrLf & " AND TDSTRN.COMPANY_CODE = TDSSECTION.COMPANY_CODE " & vbCrLf & " AND TDSTRN.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND TDSTRN.PARTYCODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND TDSTRN.FYEAR = TDSCHALLAN.FYEAR " & vbCrLf & " AND TDSTRN.CHALLANMKEY = TDSCHALLAN.MKEY " & vbCrLf & " AND TDSTRN.SECTIONCODE = TDSSECTION.CODE "
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		pSqlStr = pSqlStr & vbCrLf & " AND TDSSECTION.NAME='" & MainClass.AllowSingleQuote(LstSection.Text) & "'"
		
		pSqlStr = pSqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"
		
		If ChkAllParty.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			pSqlStr = pSqlStr & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
		End If
		
		If chkAllCerti.CheckState = System.Windows.Forms.CheckState.Checked Then
			pSqlStr = pSqlStr & vbCrLf & " AND CERTIFICATENO IS NOT NULL AND NewCertificate='Y'"
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			pSqlStr = pSqlStr & vbCrLf & " AND CERTIFICATENO='" & MainClass.AllowSingleQuote(txtCertificateNo.Text) & "'"
		End If
		
		If Trim(txtDateFrom.Text) <> "" Then
			pSqlStr = pSqlStr & vbCrLf & " AND VDate>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'"
		End If
		If Trim(txtDateTo.Text) <> "" Then
			pSqlStr = pSqlStr & vbCrLf & " AND VDate<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		End If
		
		pSqlStr = pSqlStr & vbCrLf & " ORDER BY FIN_SUPP_CUST_MST.SUPP_CUST_NAME,TDSTRN.VDATE"
		MakeSQL = pSqlStr
		Exit Function
SqlERR: 
		MsgBox(Err.Description)
	End Function
	
	'UPGRADE_WARNING: Event LstSection.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub LstSection_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles LstSection.SelectedIndexChanged
		txtCertificateNo.Text = ""
	End Sub
	
	
	
	
	Private Sub txtCertificateNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCertificateNo.DoubleClick
		Call CmdSearchCNo_Click(CmdSearchCNo, New System.EventArgs())
	End Sub
	
	
	Private Sub txtCertificateNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCertificateNo.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCNo_Click(CmdSearchCNo, New System.EventArgs())
	End Sub
	
	
	Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
		Call cmdsearch_Click(cmdsearch, New System.EventArgs())
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtCustomer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomer.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomer)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdsearch, New System.EventArgs())
	End Sub
	
	
	Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtdateFrom.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If txtDateFrom.Text = "" Then GoTo EventExitSub
		'UPGRADE_WARNING: Untranslated statement in txtdateFrom_Validate. Please check source code.
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If txtDateTo.Text = "" Then GoTo EventExitSub
		'UPGRADE_WARNING: Untranslated statement in txtDateTo_Validate. Please check source code.
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
End Class