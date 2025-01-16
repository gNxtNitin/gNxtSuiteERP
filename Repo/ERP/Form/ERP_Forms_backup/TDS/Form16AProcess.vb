Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmForm16AProcess
	Inherits System.Windows.Forms.Form
	''Dim PvtDBCn As ADODB.Connection
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub
	
	Private Sub cmdProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdProcess.Click
		On Error GoTo ERR1
		Dim mTitle As String
		Dim mSubTitle As String
		Dim SqlStr As String
		Dim mTDSAmount As Double
		
		
		
		If FieldVarification = False Then
			Exit Sub
		End If
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		If UpdateCertificateNo() = False Then GoTo ERR1
		
		'    SqlStr = " UPDATE TDS_TRN SET NewCertificate='N' " & vbCrLf _
		''            & " WHERE COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND NewCertificate='Y'"
		'    PubDBCn.Execute SqlStr
		
		MsgInformation("Process Complete.")
		PubDBCn.CommitTrans()
		
		Exit Sub
ERR1: 
		'    Resume
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgBox(Err.Description)
		PubDBCn.RollbackTrans()
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub CmdUnProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdUnProcess.Click
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim mSectionCode As Double
		Dim mQTR As String
		
		If PubSuperUser <> "S" Then
			MsgInformation("You are not Authorised to Unprocess.")
			Exit Sub
		End If
		
		If MsgQuestion("Want to unprocess All Certificate No?") = CStr(MsgBoxResult.No) Then
			Exit Sub
		End If
		
		'UPGRADE_WARNING: Untranslated statement in CmdUnProcess_Click. Please check source code.
		
		If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
			mQTR = "Q1"
		ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then 
			mQTR = "Q2"
		ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then 
			mQTR = "Q3"
		ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then 
			mQTR = "Q4"
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = " UPDATE TDS_TRN SET CERTIFICATENO='', UPDATE_FROM='" & PubRun_IN & "'," & vbCrLf & " NewCertificate='N' , " & vbCrLf & " Place='' , " & vbCrLf & " PrintDate='', " & vbCrLf & " Authorized='' , " & vbCrLf & " Authorized_FName='' , " & vbCrLf & " Authorized_Desig='',  " & vbCrLf & " ASSESSMENT_YEAR='',  " & vbCrLf & " PERIOD_FROM='',  " & vbCrLf & " PERIOD_TO='',  " & vbCrLf & " QTR_NAME='',  " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "') " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " AND SECTIONCODE=" & mSectionCode & ""
		
		If RsCompany.Fields("FYEAR").Value >= 2010 Then
			SqlStr = SqlStr & vbCrLf & " AND QTR_NAME='" & mQTR & "'"
		End If
		
		PubDBCn.BeginTrans()
		PubDBCn.Execute(SqlStr)
		PubDBCn.CommitTrans()
		
		If RsCompany.Fields("FYEAR").Value >= 2010 Then
			MsgInformation("All Certificates (Section Name " & LstSection.Text & ") " & mQTR & " are Unprocess.")
		Else
			MsgInformation("All Certificates (Section Name " & LstSection.Text & ") are Unprocess.")
		End If
		Exit Sub
ErrPart: 
		MsgInformation(Err.Description)
		PubDBCn.RollbackTrans()
	End Sub
	
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		'UPGRADE_WARNING: Untranslated statement in cmdsearch_Click. Please check source code.
	End Sub
	
	'UPGRADE_WARNING: Form event frmForm16AProcess.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmForm16AProcess_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		Me.Text = "TDS FORM 16A"
	End Sub
	Private Sub frmForm16AProcess_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		On Error GoTo ERR1
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		''Set PvtDBCn = New ADODB.Connection
		''PvtDBCn.Open StrConn
		Me.Width = VB6.TwipsToPixelsX(6015)
		Me.Height = VB6.TwipsToPixelsY(5910)
		Me.Top = 0
		Me.Left = 0
		txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
		txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		OptCustomer(0).Checked = True
		OptCustomer_CheckedChanged(OptCustomer.Item(0), New System.EventArgs())
		OptNew(0).Checked = True
		Call FillLst()
		Call FillFooter()
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
	Private Sub FillFooter()
		On Error GoTo FillERR
		Dim RsSection As ADODB.Recordset
		Dim SqlStr As String
		
		txtPlace.Text = "NEW DELHI" '''IIf(IsNull(RsCompany!COMPANY_CITY), "", RsCompany!COMPANY_CITY)
		txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtAuthorized.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtFName.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtDesignation.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
		Exit Sub
		
		
FillERR: 
		MsgBox(Err.Description)
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function FieldVarification() As Boolean
		Dim MainClass_Renamed As Object
		Dim mMonth As Integer
		Dim mDay As Integer
		Dim mLastDay As Integer
		On Error GoTo err_Renamed
		FieldVarification = True
		
		If OptCustomer(1).Checked = True Then
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
		
		If RsCompany.Fields("FYEAR").Value >= 2010 Then
			mDay = VB.Day(CDate(txtDateFrom.Text))
			
			If mDay <> 1 Then
				MsgInformation("Please Select 1st Date of the month")
				txtDateFrom.Focus()
				FieldVarification = False
				Exit Function
			End If
			
			mMonth = Month(CDate(txtDateFrom.Text))
			If mMonth = 4 Or mMonth = 7 Or mMonth = 10 Or mMonth = 1 Then
				
			Else
				MsgInformation("Please Select Quarter Starting Date")
				txtDateFrom.Focus()
				FieldVarification = False
				Exit Function
			End If
			
			mDay = VB.Day(CDate(txtDateTo.Text))
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.LastDay. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			mLastDay = MainClass.LastDay(Month(CDate(txtDateTo.Text)), Year(CDate(txtDateTo.Text)))
			If mDay <> mLastDay Then
				MsgInformation("Please Select last Date of the Quarter")
				txtDateTo.Focus()
				FieldVarification = False
				Exit Function
			End If
			
			mMonth = Month(CDate(txtDateTo.Text))
			If mMonth = 6 Or mMonth = 9 Or mMonth = 12 Or mMonth = 3 Then
				
			Else
				MsgInformation("Please Select Quarter Last Date")
				txtDateTo.Focus()
				FieldVarification = False
				Exit Function
			End If
			
			If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtDateFrom.Text), CDate(txtDateTo.Text)) + 1 <> 3 Then
				MsgInformation("Please Select Quarter Date Only")
				txtDateTo.Focus()
				FieldVarification = False
				Exit Function
			End If
		End If
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
		
		pSqlStr = " SELECT TDSTRN.MKEY, " & vbCrLf & " TDSTRN.VDATE, FIN_SUPP_CUST_MST.SUPP_CUST_NAME AS PARTYNAME, TDSTRN.AMOUNTPAID, TDSTRN.TDSRATE, " & vbCrLf & " TDSTRN.TDSAMOUNT, TDSTRN.CHALLANDATE, TDSTRN.CHALLANNO, " & vbCrLf & " TDSTRN.BANKNAME, TDSTRN.PANNO, " & vbCrLf & " TDSSECTION.NAME, TDSSECTION.NATURE," & vbCrLf & " TDSCHALLAN.FROMDATE, TDSCHALLAN.TODATE, TDSTRN.SECTIONCODE, " & vbCrLf & " Place, PrintDate, Authorized, Authorized_FName, Authorized_Desig,FIN_SUPP_CUST_MST.SUPP_CUST_ADDR, " & vbCrLf & " ASSESSMENT_YEAR, PERIOD_FROM, PERIOD_TO, QTR_NAME" & vbCrLf & " FROM  TDS_TRN TDSTRN ,  TDS_CHALLAN TDSCHALLAN, TDS_SECTION_MST TDSSECTION, FIN_SUPP_CUST_MST " & vbCrLf & " WHERE TDSTRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TDSTRN.COMPANY_CODE = TDSCHALLAN.COMPANY_CODE " & vbCrLf & " AND TDSTRN.FYEAR = TDSCHALLAN.FYEAR " & vbCrLf & " AND TDSTRN.CHALLANMKEY = TDSCHALLAN.MKEY " & vbCrLf & " AND TDSTRN.COMPANY_CODE = TDSSECTION.COMPANY_CODE " & vbCrLf & " AND TDSTRN.SECTIONCODE = TDSSECTION.CODE " & vbCrLf & " AND TDSTRN.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND TDSTRN.PARTYCODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE "
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		pSqlStr = pSqlStr & vbCrLf & " AND TDSSECTION.NAME='" & MainClass.AllowSingleQuote(LstSection.Text) & "'"
		
		pSqlStr = pSqlStr & vbCrLf & " AND TDSTRN.CANCELLED='N'"
		
		If OptCustomer(1).Checked = True Then
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			pSqlStr = pSqlStr & vbCrLf & " AND FIN_SUPP_CUST_MST.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
		End If
		
		pSqlStr = pSqlStr & vbCrLf & " AND (CERTIFICATENO='' OR CERTIFICATENO IS NULL) "
		
		If Trim(txtChallanDate.Text) <> "" Then
			pSqlStr = pSqlStr & vbCrLf & " AND TDSCHALLAN.CHALLANDATE<='" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "'"
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
	
	'UPGRADE_WARNING: Event OptCustomer.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub OptCustomer_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptCustomer.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = OptCustomer.GetIndex(eventSender)
			If Index = 0 Then
				txtCustomer.Enabled = False
				cmdSearch.Enabled = False
			Else
				txtCustomer.Enabled = True
				cmdSearch.Enabled = True
			End If
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtAuthorized_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorized.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorized)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
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
	
	Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If txtDate.Text = "" Then GoTo EventExitSub
		'UPGRADE_WARNING: Untranslated statement in txtDate_Validate. Please check source code.
EventExitSub: 
		eventArgs.Cancel = Cancel
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
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function UpdateCertificateNo() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim mPartyName As String
		Dim mCretificateNo As Integer
		Dim mCretificateNoStr As String
		Dim mQTR As String
		Dim mAssessmentYear As String
		
		SqlStr = ""
		SqlStr = MakeSQL()
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		mPartyName = ""
		If RsTemp.EOF = False Then
			If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
				mQTR = "Q1"
			ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then 
				mQTR = "Q2"
			ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then 
				mQTR = "Q3"
			ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then 
				mQTR = "Q4"
			End If
			
			If OptNew(0).Checked = True Then 'if Generate New Only
				If RsCompany.Fields("FYEAR").Value < 2010 Then
					mCretificateNo = GetMaxCretificateNo(RsTemp.Fields("SECTIONCODE").Value, "")
				Else
					mCretificateNo = GetMaxCretificateNo(RsTemp.Fields("SECTIONCODE").Value, mQTR)
				End If
			End If
			Do While Not RsTemp.EOF
				If mPartyName <> RsTemp.Fields("PARTYNAME").Value Then
					If OptNew(0).Checked = True Then 'if Generate New Only
						mCretificateNo = mCretificateNo + 1
						If RsCompany.Fields("FYEAR").Value < 2006 Then
							mCretificateNoStr = LstSection.Text & "/" & VB6.Format(Year(RunDate), "0000") & "/" & VB6.Format(mCretificateNo, "000000")
						ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then 
							mCretificateNoStr = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & VB6.Format(mCretificateNo, "000000")
						Else
							mCretificateNoStr = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & mQTR & "/" & VB6.Format(mCretificateNo, "000000")
						End If
					ElseIf OptNew(1).Checked = True Then  'if Append to latest one
						If RsCompany.Fields("FYEAR").Value < 2010 Then
							mCretificateNoStr = GetMaxPartyCretificateNo(RsTemp.Fields("SECTIONCODE").Value, RsTemp.Fields("PARTYNAME").Value, "")
						Else
							mCretificateNoStr = GetMaxPartyCretificateNo(RsTemp.Fields("SECTIONCODE").Value, RsTemp.Fields("PARTYNAME").Value, mQTR)
						End If
					End If
				End If
				
				mAssessmentYear = VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000") & "-" & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, RsCompany.Fields("END_DATE").Value), "YYYY")
				
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = " UPDATE TDS_TRN SET CERTIFICATENO='" & mCretificateNoStr & "', UPDATE_FROM='" & PubRun_IN & "'," & vbCrLf & " NewCertificate='Y' , " & vbCrLf & " Place='" & MainClass.AllowSingleQuote(txtPlace.Text) & "' , " & vbCrLf & " PrintDate=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "') , " & vbCrLf & " Authorized='" & MainClass.AllowSingleQuote(txtAuthorized.Text) & "' , " & vbCrLf & " Authorized_FName='" & MainClass.AllowSingleQuote(txtFName.Text) & "' , " & vbCrLf & " Authorized_Desig='" & MainClass.AllowSingleQuote(txtDesignation.Text) & "',  " & vbCrLf & " ASSESSMENT_YEAR='" & mAssessmentYear & "',  " & vbCrLf & " PERIOD_FROM=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'),  " & vbCrLf & " PERIOD_TO=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'),  " & vbCrLf & " QTR_NAME='" & mQTR & "',  " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "') " & vbCrLf & " WHERE MKEY='" & RsTemp.Fields("mKey").Value & "'"
				
				
				
				
				PubDBCn.Execute(SqlStr)
				mPartyName = RsTemp.Fields("PARTYNAME").Value
				RsTemp.MoveNext()
			Loop 
		End If
		UpdateCertificateNo = True
		Exit Function
ErrPart: 
		''Resume
		UpdateCertificateNo = False
		PubDBCn.RollbackTrans()
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetMaxPartyCretificateNo(ByRef pSectionCode As Integer, ByRef pPartyName As String, ByRef pQTR As String) As String
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim RsMaxPartyCNO As ADODB.Recordset
		
		'UPGRADE_WARNING: Untranslated statement in GetMaxPartyCretificateNo. Please check source code.
		
		If pQTR <> "" Then
			SqlStr = SqlStr & vbCrLf & " AND QTR_NAME='" & pQTR & "'"
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMaxPartyCNO, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsMaxPartyCNO.RecordCount > 0 Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(RsMaxPartyCNO.Fields("CNo").Value) = True Then
				'            GetMaxPartyCretificateNo = LstSection.Text & "/" & Format(Year(RunDate), "0000") & "/" & Format(1, "000000")
				
				If RsCompany.Fields("FYEAR").Value < 2006 Then
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(Year(RunDate), "0000") & "/" & VB6.Format(1, "000000")
				ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then 
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & VB6.Format(1, "000000")
				Else
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & pQTR & "/" & VB6.Format(1, "000000")
				End If
				
				
			Else
				'            GetMaxPartyCretificateNo = RsMaxPartyCNO!CNo
				If RsCompany.Fields("FYEAR").Value < 2006 Then
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(Year(RunDate), "0000") & "/" & VB6.Format(RsMaxPartyCNO.Fields("CNo").Value, "000000")
				ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then 
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & VB6.Format(RsMaxPartyCNO.Fields("CNo").Value, "000000")
				Else
					GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & pQTR & "/" & VB6.Format(RsMaxPartyCNO.Fields("CNo").Value, "000000")
				End If
			End If
		Else
			'        GetMaxPartyCretificateNo = LstSection.Text & "/" & Format(Year(RunDate), "0000") & "/" & Format(1, "000000")
			If RsCompany.Fields("FYEAR").Value < 2006 Then
				GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(Year(RunDate), "0000") & "/" & VB6.Format(1, "000000")
			ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then 
				GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & VB6.Format(1, "000000")
			Else
				GetMaxPartyCretificateNo = LstSection.Text & "/" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00") & "-" & VB6.Format(RsCompany.Fields("FYEAR").Value + 1, "00") & "/" & pQTR & "/" & VB6.Format(1, "000000")
			End If
		End If
		
		
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		GetMaxPartyCretificateNo = ""
		'    Resume
	End Function
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetMaxCretificateNo(ByRef pSectionCode As Integer, ByRef pQTR As String) As Integer
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim RsMax As ADODB.Recordset
		
		SqlStr = "Select MAX(SUBSTR(CERTIFICATENO,LENGTH(CERTIFICATENO)-5)) as CNo from TDS_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND SECTIONCODE=" & pSectionCode & ""
		
		If pQTR <> "" Then
			SqlStr = SqlStr & vbCrLf & " AND QTR_NAME='" & pQTR & "'"
		End If
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMax, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsMax.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			GetMaxCretificateNo = Val(IIf(IsDbNull(RsMax.Fields("CNo").Value), 0, RsMax.Fields("CNo").Value))
		Else
			GetMaxCretificateNo = 0
		End If
		
		Exit Function
ErrPart: 
		GetMaxCretificateNo = 0
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtDesignation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesignation.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtDesignation)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorized)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtPlace_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlace.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtPlace)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class