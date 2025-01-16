Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTDSForm26
	Inherits System.Windows.Forms.Form
	Dim XRIGHT As String
	'Dim PvtDBCn As ADODB.Connection
	
	Private Const RowHeight As Short = 15
	
	Dim mActiveRow As Integer
	Dim FormActive As Boolean
	Private Const mPageWidth As Short = 135
	
	Private Sub PrintStatus(ByRef pPrintEnable As Boolean)
		CmdPreview.Enabled = pPrintEnable
		cmdPrint.Enabled = pPrintEnable
	End Sub
	Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
		Me.Close()
	End Sub
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		''    If mDOSPRINTING = True Then
		'        Call ShowDosReport("V")
		''    Else
		Call ReportForTDS(Crystal.DestinationConstants.crptToWindow)
		'    End If
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ReportForTDS(ByRef Mode As Crystal.DestinationConstants)
		Dim MainClass_Renamed As Object
		
		On Error GoTo ERR1
		Dim All As Boolean
		Dim SqlStr As String
		Dim mTitle As String
		Dim mSubTitle As String
		Dim PrintStatus As Boolean
		Dim mReportFileName As String
		
		PubDBCn.Errors.Clear()
		
		PrintStatus = False
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		PubDBCn.Execute(SqlStr)
		
		SqlStr = ""
		
		Call InsertIntoPrintDummy()
		
		'''''Select Record for print...
		
		SqlStr = ""
		
		SqlStr = FetchRecordForReport(SqlStr)
		
		Select Case lblFormType.Text
			Case "26A"
				mTitle = "FORM NO. 26A"
				mSubTitle = "[See section 194A and rule 37"
			Case "26B"
				mTitle = ""
				mSubTitle = ""
			Case "26C"
				mTitle = "FORM NO. 26C"
				mSubTitle = "[See section 194C and rule 37"
			Case "26D"
				mTitle = ""
				mSubTitle = ""
			Case "26E"
				mTitle = ""
				mSubTitle = ""
			Case "26F"
				mTitle = ""
				mSubTitle = ""
			Case "26G"
				mTitle = ""
				mSubTitle = ""
			Case "26H"
				mTitle = ""
				mSubTitle = ""
			Case "26I"
				mTitle = "FORM NO. 26I"
				mSubTitle = "[See Section 194H and Rule 37"
			Case "26J"
				mTitle = "FORM NO. 26J"
				mSubTitle = "[See Section 194I and Rule 37"
			Case "26K"
				mTitle = "FORM NO. 26K"
				mSubTitle = "[See Section 194J and Rule 37"
		End Select
		
		
		If lblFormType.Text = "26C" Then
			mReportFileName = "TDSForm26C.Rpt"
		ElseIf lblFormType.Text = "26A" Then 
			mReportFileName = "TDSForm26A.Rpt"
		Else
			mReportFileName = "TDSForm26.Rpt"
		End If
		
		Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		
		PrintStatus = True
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
	Private Function InsertDetail1() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		Dim mTDSACNo As String
		Dim mPANNo As String
		Dim mCompanyName As String
		Dim mFlatNo As String
		Dim mBuilding As String
		Dim mRoad As String
		Dim mArea As String
		Dim mTown As String
		Dim mState As String
		Dim mPinCode As String
		Dim mSumLastReturn As String
		
		SqlStr = ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTDSACNo = MainClass.AllowSingleQuote(Trim(txtTDSNo.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mPANNo = MainClass.AllowSingleQuote(Trim(txtPanNo.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCompanyName = MainClass.AllowSingleQuote(Trim(txtCompanyName.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mFlatNo = MainClass.AllowSingleQuote(Trim(txtFlat.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mBuilding = MainClass.AllowSingleQuote(Trim(txtBuilding.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mRoad = MainClass.AllowSingleQuote(Trim(txtRoad.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mArea = MainClass.AllowSingleQuote(Trim(txtArea.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mTown = MainClass.AllowSingleQuote(Trim(txtTown.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mState = MainClass.AllowSingleQuote(Trim(txtState.Text))
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mPinCode = MainClass.AllowSingleQuote(Trim(txtPinCode.Text))
		mSumLastReturn = IIf(ChkYes.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " 1, " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTDSACNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPANNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCompanyName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mFlatNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mBuilding) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRoad) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mArea) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTown) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mState) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPinCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSumLastReturn) & "')"
		
		PubDBCn.Execute(SqlStr)
		InsertDetail1 = True
		Exit Function
ERR1: 
		'Resume
		MsgInformation(Err.Description)
		InsertDetail1 = False
	End Function
	Private Sub InsertIntoPrintDummy()
		On Error GoTo ERR1
		Dim mRowTitle As String
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		If InsertDetail1 = False Then GoTo ERR1
		
		'''''''''********************************************************
		
		mRowTitle = lbl3A.Text
		If InsertGridDetail(SprdView3, 3, (SprdView3.MaxRows), (SprdView3.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		If lblFormType.Text = "26A" Then
			If InsertLabel((lbl26A4A.Text), txt26A4A.Text, 31) = False Then GoTo ERR1
			If InsertLabel((lbl26A4B.Text), txt26A4B.Text, 32) = False Then GoTo ERR1
			If InsertLabel((lbl26A4C.Text), txt26A4C.Text, 33) = False Then GoTo ERR1
			If InsertLabel((lbl26A4D.Text), txt26A4D.Text, 34) = False Then GoTo ERR1
			If InsertLabel((lbl26A4E.Text), txt26A4E.Text, 35) = False Then GoTo ERR1
		End If
		'''''''''********************************************************
		
		mRowTitle = lbl4.Text & vbCrLf & vbCrLf & lbl4A.Text
		
		If InsertGridDetail(SprdView4A, 41, (SprdView4A.MaxRows), (SprdView4A.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		
		mRowTitle = lbl4B.Text
		
		If InsertGridDetail(SprdView4B, 42, (SprdView4B.MaxRows), (SprdView4B.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		mRowTitle = lbl5.Text & vbCrLf & vbCrLf & lbl5A.Text
		
		If InsertGridDetail(SprdView5A, 51, (SprdView5A.MaxRows), (SprdView5A.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		mRowTitle = lbl5B.Text
		
		If InsertGridDetail(SprdView5B, 52, (SprdView5B.MaxRows), (SprdView5B.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		mRowTitle = lbl6.Text & vbCrLf & vbCrLf & lbl6A.Text
		
		If InsertGridDetail(SprdView6A, 61, (SprdView6A.MaxRows), (SprdView6A.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		mRowTitle = lbl6B.Text
		
		If InsertGridDetail(SprdView6B, 62, (SprdView6B.MaxRows), (SprdView6B.MaxCols), mRowTitle) = False Then GoTo ERR1
		
		'''''''''********************************************************
		If lblFormType.Text = "26A" Then
			mRowTitle = lbl7.Text & vbCrLf & vbCrLf & lbl7A.Text
			
			If InsertGridDetail(SprdView7A, 71, (SprdView7A.MaxRows), (SprdView7A.MaxCols), mRowTitle) = False Then GoTo ERR1
		End If
		
		PubDBCn.CommitTrans()
		Exit Sub
ERR1: 
		'Resume
		PubDBCn.RollbackTrans()
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
		Dim MainClass_Renamed As Object
		Dim mFormTitle As String
		
		
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		mFormTitle = "Annual return of deduction of tax from fees for professional or technical services, under section 206 of the Income-tax Act, 1961, for the year ending 31st March"
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Label2C=""" & Trim(lbl2C.Text) & """")
		' Report1.CopiesToPrinter = PrintCopies
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		
		Report1.MarginLeft = 0
		Report1.MarginRight = 0
		
		Report1.Action = 1
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"
		
		FetchRecordForReport = mSqlStr
		
	End Function
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'    If mDOSPRINTING = True Then
		'        Call ShowDosReport("V")
		'    Else
		Call ReportForTDS(Crystal.DestinationConstants.crptToPrinter)
		'    End If
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
		If FieldsVerification = False Then Exit Sub
		
		Call PrintStatus(False)
		Call Clear1()
		
		FillForm26Title()
		Show1()
		FormatSprdView()
		
		Call PrintStatus(True)
	End Sub
	Function FieldsVerification() As Boolean
		On Error GoTo ERR1
		
		FieldsVerification = True
		Exit Function
ERR1: 
		FieldsVerification = False
	End Function
	'UPGRADE_WARNING: Form event frmTDSForm26.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Public Sub frmTDSForm26_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		On Error GoTo ERR1
		If FormActive = True Then Exit Sub
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		If lblFormType.Text = "26A" Then
			SSTab1.TabPages.Item(7).Visible = True
			Fra26A.Visible = True
		Else
			SSTab1.TabPages.Item(7).Visible = False
			Fra26A.Visible = False
		End If
		Call PrintStatus(False)
		lblTile.ForeColor = System.Drawing.Color.Blue
		FillForm26Title()
		FormatSprdView()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		FormActive = True
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTDSForm26_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		On Error GoTo BSLError
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
		Me.Top = 0
		Me.Left = 0
		Me.Height = VB6.TwipsToPixelsY(6195)
		Me.Width = VB6.TwipsToPixelsX(10155)
		SSTab1.SelectedIndex = 0
		
		Call PrintStatus(True)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
BSLError: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgInformation(Err.Description)
	End Sub
	Private Sub Show1()
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim mSectionCode As Integer
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		'UPGRADE_WARNING: Untranslated statement in Show1. Please check source code.
		If ShowDetail1 = False Then GoTo ErrPart
		If ShowDetail3(mSectionCode) = False Then GoTo ErrPart
		If ShowDetail4(mSectionCode) = False Then GoTo ErrPart
		If ShowDetail5(SprdView5A, mSectionCode, "C", AData5A) = False Then GoTo ErrPart
		If ShowDetail5(SprdView5B, mSectionCode, "N", AData5B) = False Then GoTo ErrPart
		If ShowDetail6(SprdView6A, mSectionCode, "C", AData6A) = False Then GoTo ErrPart
		If ShowDetail6(SprdView6B, mSectionCode, "N", AData6B) = False Then GoTo ErrPart
		If ShowDetail7(SprdView7A, mSectionCode, "N", AData7A) = False Then GoTo ErrPart
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgInformation(Err.Description)
		
	End Sub
	Private Function MakeSQL() As String
		On Error GoTo ERR1
		Dim SqlStr As String
		
		SqlStr = ""
		MakeSQL = SqlStr
		Exit Function
ERR1: 
		MsgInformation(Err.Description)
		MakeSQL = ""
	End Function
	Private Sub FormatSprdView()
		Call FormatSprdView3()
		Call FormatSprdView4A()
		Call FormatSprdView4B()
		Call FormatSprdView5A()
		Call FormatSprdView5B()
		Call FormatSprdView6A()
		Call FormatSprdView6B()
		Call FormatSprdView7A()
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView3()
		Dim MainClass_Renamed As Object
		With SprdView3
			.MaxCols = 8 ''IIf(lblFormType.Caption = "26C", 8, 7)
			
			.set_RowHeight(0, RowHeight * 4)
			If lblFormType.Text = "26A" Then
				.set_RowHeight(0, RowHeight * 4)
				.Height = VB6.TwipsToPixelsY(2595)
			Else
				.set_RowHeight(0, RowHeight * 7)
				.Height = VB6.TwipsToPixelsY(4200)
			End If
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 30)
			.ColsFrozen = 1
			
			.Col = 2
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 4
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 10)
			
			.Col = 6
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 10)
			
			.Col = 7
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 10)
			
			
			.Col = 8
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 10)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			FillHeadingSprdView3()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView3, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView3, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView4A()
		Dim MainClass_Renamed As Object
		With SprdView4A
			.MaxCols = 3
			
			.set_RowHeight(0, RowHeight * 2)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 25)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 19)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 19)
			
			FillHeadingSprdView4A()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView4A, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView4A, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
			.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView4B()
		Dim MainClass_Renamed As Object
		With SprdView4B
			.MaxCols = 4
			
			.set_RowHeight(0, RowHeight * 2)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 10)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 10)
			
			.Col = 3
			'        .CellType = SS_CELL_TYPE_EDIT
			'        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
			'        .TypeMaxEditLen = 255
			'        .TypeEditMultiLine = True
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 17)
			
			.Col = 4
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 26)
			
			FillHeadingSprdView4B()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView4B, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView4B, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
			.ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsVertical
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView5A()
		Dim MainClass_Renamed As Object
		With SprdView5A
			.MaxCols = 12
			
			.set_RowHeight(0, RowHeight * 5)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 12)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 4
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 15)
			
			.Col = 6
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 7
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 8
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 9
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 10
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 11
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			FillHeadingSprdView5A()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView5A, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView5A, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView5B()
		Dim MainClass_Renamed As Object
		With SprdView5B
			.MaxCols = 12
			
			.set_RowHeight(0, RowHeight * 5)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 12)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 4
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 15)
			
			.Col = 6
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 7
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 8
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 9
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 10
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 11
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			FillHeadingSprdView5B()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView5B, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView5B, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView6A()
		Dim MainClass_Renamed As Object
		With SprdView6A
			If lblFormType.Text = "26I" Then
				.MaxCols = 5
			Else
				.MaxCols = 14
			End If
			
			
			.set_RowHeight(0, RowHeight * 5)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 12)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 4
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 15)
			
			.Col = 6
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 7
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 8
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 9
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 10
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 11
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 13
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			.Col = 14
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			FillHeadingSprdView6A()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView6A, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView6A, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView6B()
		Dim MainClass_Renamed As Object
		With SprdView6B
			If lblFormType.Text = "26I" Then
				.MaxCols = 5
			Else
				.MaxCols = 14
			End If
			
			
			.set_RowHeight(0, RowHeight * 5)
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 12)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 4
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 15)
			
			.Col = 6
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 7
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			Else
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 8
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 9
			If lblFormType.Text = "26C" Then
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMax = CDbl("9999999.99")
				.TypeFloatMin = CDbl("-9999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			Else
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.TypeEditMultiLine = True
			End If
			.set_ColWidth(.Col, 12)
			
			.Col = 10
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 11
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			
			.Col = 13
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			.Col = 14
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 12)
			.ColHidden = IIf(lblFormType.Text = "26C", False, True)
			
			FillHeadingSprdView6B()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView6B, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView6B, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView7A()
		Dim MainClass_Renamed As Object
		Dim m As Double
		With SprdView7A
			.MaxCols = 7
			
			.set_RowHeight(0, RowHeight * 5)
			
			.set_ColWidth(0, 5)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 12)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 25)
			
			.Col = 4
			.CellType = SS_CELL_TYPE_FLOAT
			.TypeFloatDecimalChar = Asc(".")
			.TypeFloatDecimalPlaces = 2
			.TypeFloatMax = CDbl("9999999.99")
			.TypeFloatMin = CDbl("-9999999.99")
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
			.set_ColWidth(.Col, 12)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 15)
			
			.Col = 6
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 18)
			
			.Col = 7
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeEditMultiLine = True
			.set_ColWidth(.Col, 22)
			
			
			FillHeadingSprdView7A()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView7A, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView7A, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	Private Sub frmTDSForm26_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		FormActive = False
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		txtArea.Text = ""
		txtBuilding.Text = ""
		txtCompanyName.Text = ""
		txtFlat.Text = ""
		txtPanNo.Text = ""
		txtPinCode.Text = ""
		txtRoad.Text = ""
		txtState.Text = ""
		txtTDSNo.Text = ""
		txtTown.Text = ""
		ChkYes.CheckState = System.Windows.Forms.CheckState.Unchecked
		ChkNo.CheckState = System.Windows.Forms.CheckState.Unchecked
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView3, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView4A, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView4B, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView5A, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView5B, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView6A, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView6B, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView7A, RowHeight)
	End Sub
	Private Sub FillHeadingSprdView3()
		
		With SprdView3
			.Row = 0
			
			.Col = 1
			.Text = "Payee"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Gross amount of interest paid during the year" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26C"
					.Text = "Gross sum of payment made" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26I"
					.Text = "Gross amount of commission (not being insurance commission) or brokerage credited / paid during the year " & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26J"
					.Text = "Gross amount of rent credited / paid during the year" & vbNewLine & "(Rs.)" & vbNewLine & "(1)"
				Case "26K"
					.Text = "Gross amount of fees for professional or technical services credited / paid during the year " & vbNewLine & "(Rs)" & vbNewLine & "(1)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Total amount of interest on which no tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26C"
					.Text = "Total sums on which no tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26I"
					.Text = "Total amount of commission (not being insurance commission) or brokerage credited / paid on which no tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26J"
					.Text = "Total rent credit / paid on which no tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
				Case "26K"
					.Text = "Total fees for professional or technical services credited / paid on which no tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(2)"
			End Select
			
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Total amount of interest on which tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26C"
					.Text = "Amount" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26I"
					.Text = "Total amount of commission (not being insurance commission) or brokerage credited / paid on which tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26J"
					.Text = "Total rent credited / paid on which tax deducted" & vbNewLine & "(Rs)" & vbNewLine & "(3)"
				Case "26K"
					.Text = "Total fees for professional or technical services credited / paid on which tax deducted " & vbNewLine & "(Rs)" & vbNewLine & "(3)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			If lblFormType.Text = "26C" Then
				.Col = 5
				.Text = "Number of persons" & vbNewLine & "(4)"
				.Font = VB6.FontChangeBold(.Font, True)
			End If
			
			.Col = IIf(lblFormType.Text = "26C", 6, 5)
			.Text = "Income - tax " & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(5)", "(4)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 7, 6)
			.Text = "Surcharge " & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(6)", "(5)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 8, 7)
			.Text = "Total " & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(7)", "(6)")
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub FillHeadingSprdView4A()
		
		With SprdView4A
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Amount of Tax Deducted " & vbNewLine & "(Rs)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Transfer Voucher Number" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Date of Transfer Voucher" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub FillHeadingSprdView4B()
		
		With SprdView4B
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Challan No." & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Date of Payment" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Amount of Tax Paid" & vbNewLine & "(Rs)" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Name and Address of Bank" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub FillHeadingSprdView6B()
		
		With SprdView6B
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Permanent Account Number (PAN)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Name of person / payee" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Address of person payee" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Amount of Interest" & vbNewLine & "(5)"
				Case "26B"
					.Text = "" & vbNewLine & "(5)"
				Case "26C"
					.Text = "Type of payee" & vbNewLine & "(5)"
				Case "26D"
					.Text = "" & vbNewLine & "(5)"
				Case "26E"
					.Text = "" & vbNewLine & "(5)"
				Case "26F"
					.Text = "" & vbNewLine & "(5)"
				Case "26G"
					.Text = "" & vbNewLine & "(5)"
				Case "26H"
					.Text = "" & vbNewLine & "(5)"
				Case "26I"
					.Text = "Amount of commission (not being Insurance commission) or brokerage credited / paid" & vbNewLine & "(5)"
				Case "26J"
					.Text = "Amount of rent credited / paid whichever is earlier" & vbNewLine & "(5)"
				Case "26K"
					.Text = "Amount of fees for professional or technical services credited or paid " & vbNewLine & "(5)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Date on which Interest credited or paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26B"
					.Text = "" & vbNewLine & "(6)"
				Case "26C"
					.Text = "Gross value of the contract / subcontract" & vbNewLine & "(Rs)" & vbNewLine & "(6)"
				Case "26D"
					.Text = "" & vbNewLine & "(6)"
				Case "26E"
					.Text = "" & vbNewLine & "(6)"
				Case "26F"
					.Text = "" & vbNewLine & "(6)"
				Case "26G"
					.Text = "" & vbNewLine & "(6)"
				Case "26H"
					.Text = "" & vbNewLine & "(6)"
				Case "26I"
					.Text = "Date on which amount of commission (not being Insurance commission) or brokerage credited / paid Whichever is earlier" & vbNewLine & "(6)"
					.Font = VB6.FontChangeBold(.Font, True)
					Exit Sub
				Case "26J"
					.Text = "Date on which amount of rent credited / paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26K"
					.Text = "Date on which amount of fees for professional or technical services credited / paid which ever is earlier" & vbNewLine & "(6)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			If lblFormType.Text = "26C" Then
				.Col = 6
				.Text = "Sums paid / credited " & vbNewLine & "(Rs)" & vbNewLine & "(7)"
				.Font = VB6.FontChangeBold(.Font, True)
				
				.Col = 7
				.Text = "Date on which sums credited / paid, which ever is earlier" & vbNewLine & "(Rs)" & vbNewLine & "(8)"
				.Font = VB6.FontChangeBold(.Font, True)
			End If
			
			.Col = IIf(lblFormType.Text = "26C", 8, 6)
			.Text = "Rate of Ded. of Tax (%)" & vbNewLine & IIf(lblFormType.Text = "26C", "(9)", "(7)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 9, 7)
			.Text = "Amount of tax deducted " & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(10)", "(8)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 10, 8)
			.Text = "Date on which tax is deducted" & vbNewLine & IIf(lblFormType.Text = "26C", "(11)", "(9)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 11, 9)
			.Text = "Date on which tax was paid to the credit of the Central Govt." & vbNewLine & IIf(lblFormType.Text = "26C", "(12)", "(10)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 12, 10)
			.Text = "Assessing Officer's Certificate Reference Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(13)", "(11)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 13, 11)
			.Text = "Tax Deduction Certificate Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(14)", "(12)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 14, 12)
			.Text = "Date of furnishing of Tax Deduction Certificate to the person / payee" & vbNewLine & IIf(lblFormType.Text = "26C", "(15)", "(13)")
			.Font = VB6.FontChangeBold(.Font, True)
		End With
	End Sub
	
	Private Sub FillHeadingSprdView7A()
		
		With SprdView7A
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Permanent Account Number (PAN)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Name of person / payee" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Address of person payee" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Amount of Interest" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Date(s) of Credit or payment of interest" & vbNewLine & "(6)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Date on which the declaration was furnished to the person responsible for paying the interest" & vbNewLine & "(Rs)" & vbNewLine & "(7)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Date on which the declaration was furnished to Commissioner of Income-tax / Chief Commissioner of Income-tax" & vbNewLine & "(Rs)" & vbNewLine & "(8)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			
			
		End With
	End Sub
	Private Sub FillHeadingSprdView6A()
		
		With SprdView6A
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Permanent Account Number (PAN)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Name of Company" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Address of company" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Amount of Interest" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26C"
					.Text = "Type of company" & vbNewLine & "(5)"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26I"
					.Text = "Amount of commission (not being Insurance commission) or brokerage credited / paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26J"
					.Text = "Amount of rent credited or paid, whichever is earlier" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26K"
					.Text = "Amount of fees for professional or technical services credited or paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Date on which interest credited or paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26B"
					.Text = "" & vbNewLine & "(6)"
				Case "26C"
					.Text = "Gross value of the contract / sub-contract" & vbNewLine & "(Rs)" & vbNewLine & "(6)"
				Case "26D"
					.Text = "" & vbNewLine & "(6)"
				Case "26E"
					.Text = "" & vbNewLine & "(6)"
				Case "26F"
					.Text = "" & vbNewLine & "(6)"
				Case "26G"
					.Text = "" & vbNewLine & "(6)"
				Case "26H"
					.Text = "" & vbNewLine & "(6)"
				Case "26I"
					.Text = "Date on which amount of commission (not being insurance commission) or brokerage credited of paid whichever is earlier" & vbNewLine & "(6)"
					.Font = VB6.FontChangeBold(.Font, True)
					Exit Sub
				Case "26J"
					.Text = "Date on which amount of rate credited / paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26K"
					.Text = "Date on which amount of fees for professional or technical services credited / paid, whichever is earlier" & vbNewLine & "(6)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			If lblFormType.Text = "26C" Then
				.Col = 6
				.Text = "Amount paid / credited " & vbNewLine & "(Rs)" & vbNewLine & "(7)"
				.Font = VB6.FontChangeBold(.Font, True)
				
				.Col = 7
				.Text = "Date on which sums credited / paid, whichever is earlier" & vbNewLine & "(8)"
				.Font = VB6.FontChangeBold(.Font, True)
			End If
			
			.Col = IIf(lblFormType.Text = "26C", 8, 6)
			.Text = "Rate of Ded. of Tax (%)" & vbNewLine & IIf(lblFormType.Text = "26C", "(9)", "(7)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 9, 7)
			.Text = "Amount of tax deducted" & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(10)", "(8)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 10, 8)
			.Text = "Date on which tax deducted" & vbNewLine & IIf(lblFormType.Text = "26C", "(11)", "(9)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 11, 9)
			.Text = "Date on which tax was paid to the credit of the Central Govt." & vbNewLine & IIf(lblFormType.Text = "26C", "(12)", "(10)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 12, 10)
			.Text = "Assessing Officer's Certificate Reference Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(13)", "(11)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 13, 11)
			.Text = "Tax Deduction Certificate Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(14)", "(12)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 14, 12)
			.Text = "Date of furnishing of Tax Deduction Certificate to the company" & vbNewLine & IIf(lblFormType.Text = "26C", "(15)", "(13)")
			.Font = VB6.FontChangeBold(.Font, True)
		End With
	End Sub
	Private Sub FillHeadingSprdView5B()
		
		With SprdView5B
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Permanent Account Number (PAN)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Name of person / payee" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Address of person / payee" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Amount of Interest" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26C"
					.Text = "Type of payee" & vbNewLine & "(5)"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26I"
					.Text = "Amount of commission (not being Insurance commission) or brokerage credit / paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26J"
					.Text = "Amount of rent credited or paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26K"
					.Text = "Amount of fees for professional or technical services credited or paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Date on which interest credited or paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26B"
					.Text = "" & vbNewLine & "(6)"
				Case "26C"
					.Text = "Gross value of the contract/ subcontract" & vbNewLine & "(Rs)" & vbNewLine & "(6)"
				Case "26D"
					.Text = "" & vbNewLine & "(6)"
				Case "26E"
					.Text = "" & vbNewLine & "(6)"
				Case "26F"
					.Text = "" & vbNewLine & "(6)"
				Case "26G"
					.Text = "" & vbNewLine & "(6)"
				Case "26H"
					.Text = "" & vbNewLine & "(6)"
				Case "26I"
					.Text = "Date on which amount of commission (not being Insurance commission) or brokerage credited / paid whichever is earlier" & vbNewLine & "(6)"
				Case "26J"
					.Text = "Date on which amount of rent credited / paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26K"
					.Text = "Date on which amount of fees for professional or technical services credited / paid, whichever is earlier" & vbNewLine & "(6)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			If lblFormType.Text = "26C" Then
				.Col = 6
				.Text = "Sums paid / credited" & vbNewLine & "(Rs)" & vbNewLine & "(7)"
				.Font = VB6.FontChangeBold(.Font, True)
				
				.Col = 7
				.Text = "Date on which sums credited / paid, whichever is earlier" & vbNewLine & "(8)"
				.Font = VB6.FontChangeBold(.Font, True)
			End If
			
			.Col = IIf(lblFormType.Text = "26C", 8, 6)
			.Text = "Amount of tax deducted" & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(9)", "(7)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 9, 7)
			.Text = "Date on which tax deducted" & vbNewLine & IIf(lblFormType.Text = "26C", "(10)", "(8)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 10, 8)
			.Text = "Date on which tax was paid to the credit of the Central Govt." & vbNewLine & IIf(lblFormType.Text = "26C", "(11)", "(9)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 11, 9)
			.Text = "Tax Deduction Certificate Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(12)", "(10)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 12, 10)
			.Text = "Date of furnishing of Tax Deduction Certificate to the person / payee" & vbNewLine & IIf(lblFormType.Text = "26C", "(13)", "(11)")
			.Font = VB6.FontChangeBold(.Font, True)
		End With
	End Sub
	Private Sub FillHeadingSprdView5A()
		
		With SprdView5A
			.Row = 0
			
			.Col = 0
			.Text = "SL. No." & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 1
			.Text = "Permanent Account Number (PAN)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Name of Company" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Address of Company" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Amount of Interest" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26B"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26C"
					.Text = "Type of company"
				Case "26D"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26E"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26F"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26G"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26H"
					.Text = "" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26I"
					.Text = "Amount of commission (not being insurance commission) or brokerage credited / paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26J"
					.Text = "Amount of rent paid / credited" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
				Case "26K"
					.Text = "Amount of fees for professional or technical services credited or paid" & vbNewLine & "(Rs)" & vbNewLine & "(5)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			Select Case lblFormType.Text
				Case "26A"
					.Text = "Date on which interest credited or paid, whichever is earlier"
				Case "26B"
					.Text = "" & vbNewLine & "(6)"
				Case "26C"
					.Text = "Gross value of the contract / subcontract" & vbNewLine & "(Rs)" & vbNewLine & "(6)"
				Case "26D"
					.Text = "" & vbNewLine & "(6)"
				Case "26E"
					.Text = "" & vbNewLine & "(6)"
				Case "26F"
					.Text = "" & vbNewLine & "(6)"
				Case "26G"
					.Text = "" & vbNewLine & "(6)"
				Case "26H"
					.Text = "" & vbNewLine & "(6)"
				Case "26I"
					.Text = "Date on which amount of commission (not being Insurance commission) or brokerage credited of paid whichever is earlier" & vbNewLine & "(6)"
				Case "26J"
					.Text = "Date on which amount of rent credited / paid, whichever is earlier" & vbNewLine & "(6)"
				Case "26K"
					.Text = "Date on which amount of fees for professional or technical services credited / paid, whichever is earlier" & vbNewLine & "(6)"
			End Select
			.Font = VB6.FontChangeBold(.Font, True)
			
			If lblFormType.Text = "26C" Then
				.Col = 6
				.Text = "Sums paid / credited" & vbNewLine & "(Rs)" & vbNewLine & "(7)"
				.Font = VB6.FontChangeBold(.Font, True)
				
				.Col = 7
				.Text = "Date on which sums credited / paid, whichever is earlier" & vbNewLine & "(8)"
				.Font = VB6.FontChangeBold(.Font, True)
				
			End If
			
			.Col = IIf(lblFormType.Text = "26C", 8, 6)
			.Text = "Amount of tax deducted" & vbNewLine & "(Rs)" & vbNewLine & IIf(lblFormType.Text = "26C", "(9)", "(7)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 9, 7)
			.Text = "Date on which tax deducted" & vbNewLine & IIf(lblFormType.Text = "26C", "(10)", "(8)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 10, 8)
			.Text = "Date on which tax was paid to the credit of the Central Govt." & vbNewLine & IIf(lblFormType.Text = "26C", "(11)", "(9)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 11, 9)
			.Text = "Tax Deduction Certificate Number" & vbNewLine & IIf(lblFormType.Text = "26C", "(12)", "(10)")
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = IIf(lblFormType.Text = "26C", 12, 10)
			.Text = "Date of furnishing of Tax Deduction Certificate to the Company" & vbNewLine & IIf(lblFormType.Text = "26C", "(13)", "(11)")
			.Font = VB6.FontChangeBold(.Font, True)
		End With
	End Sub
	Private Function ShowDetail1() As Boolean
		On Error GoTo ErrPart1
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTDSNo.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPanNo.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtCompanyName.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_NAME").Value), "", RsCompany.Fields("COMPANY_NAME").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtBuilding.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTown.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtState.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPinCode.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
		ShowDetail1 = True
		Exit Function
ErrPart1: 
		ShowDetail1 = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail3(ByRef pSectionCode As Integer) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RS As ADODB.Recordset
		Dim mTotAmountPaid As Double
		Dim mTotAmountPaidWOTax As Double
		Dim mTotAmountPaidWTax As Double
		Dim mTotTDSAmount As Double
		
		
		SqlStr = "Select SUM(AMOUNTPAID) AS AMOUNTPAID, SUM(DECODE(ISEXEPTED,'Y',1,0)*AMOUNTPAID) AS AMOUNTPAIDWOTAX, " & vbCrLf & " SUM(DECODE(ISEXEPTED,'N',1,0)*AMOUNTPAID) AS AMOUNTPAIDWTAX, " & vbCrLf & " SUM(TDSAMOUNT) AS TDSAMOUNT,CTYPE " & vbCrLf & " FROM TDS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SECTIONCODE=" & pSectionCode & " AND CHALLANMKEY IS NOT NULL" & vbCrLf & " AND CANCELLED='N' " & vbCrLf & " GROUP BY CTYPE ORDER BY CTYPE"
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RS.EOF = False Then
			With SprdView3
				.MaxRows = 3
				
				.Col = 1
				.Row = 1
				.Text = "1. Companies"
				
				.Row = 2
				.Text = "2. Persons other than Companies"
				
				Do While Not RS.EOF
					If RS.Fields("CType").Value = "C" Then
						.Row = 1
					Else
						.Row = 2
					End If
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RS.Fields("AmountPaid").Value), 0, RS.Fields("AmountPaid").Value), "0.00")
					mTotAmountPaid = mTotAmountPaid + Val(.Text)
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RS.Fields("AMOUNTPAIDWOTAX").Value), 0, RS.Fields("AMOUNTPAIDWOTAX").Value), "0.00")
					mTotAmountPaidWOTax = mTotAmountPaidWOTax + Val(.Text)
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RS.Fields("AMOUNTPAIDWTAX").Value), 0, RS.Fields("AMOUNTPAIDWTAX").Value), "0.00")
					mTotAmountPaidWTax = mTotAmountPaidWTax + Val(.Text)
					
					.Col = IIf(lblFormType.Text = "26C", 6, 5)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RS.Fields("TDSAMOUNT").Value), 0, RS.Fields("TDSAMOUNT").Value), "0.00")
					
					.Col = IIf(lblFormType.Text = "26C", 8, 7)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RS.Fields("TDSAMOUNT").Value), 0, RS.Fields("TDSAMOUNT").Value), "0.00")
					mTotTDSAmount = mTotTDSAmount + Val(.Text)
					RS.MoveNext()
				Loop 
			End With
		End If
		
		SprdView3.Row = 3
		
		SprdView3.Col = 1
		SprdView3.Text = "TOTAL"
		
		
		SprdView3.Col = 2
		SprdView3.Text = VB6.Format(mTotAmountPaid, "0.00")
		
		SprdView3.Col = 3
		SprdView3.Text = VB6.Format(mTotAmountPaidWOTax, "0.00")
		
		SprdView3.Col = 4
		SprdView3.Text = VB6.Format(mTotAmountPaidWTax, "0.00")
		
		SprdView3.Col = IIf(lblFormType.Text = "26C", 6, 5)
		SprdView3.Text = VB6.Format(mTotTDSAmount, "0.00")
		
		SprdView3.Col = IIf(lblFormType.Text = "26C", 8, 7)
		SprdView3.Text = VB6.Format(mTotTDSAmount, "0.00")
		
		ShowDetail3 = True
		Exit Function
ErrPart1: 
		ShowDetail3 = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail4(ByRef pSectionCode As Integer) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RS As ADODB.Recordset
		Dim mTotAmountPaid As Double
		Dim mTotTDSAmount As Double
		
		SqlStr = "Select CHALLANNO , CHALLANDATE ,TO_CHAR(SUM(TDSAMOUNT)) AS TDSAMOUNT,BANKNAME " & vbCrLf & " FROM TDS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SECTIONCODE=" & pSectionCode & " AND CHALLANMKEY IS NOT NULL " & vbCrLf & " AND CANCELLED='N' " & vbCrLf & " GROUP BY CHALLANNO , CHALLANDATE,BANKNAME"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, AData1, StrConn, "Y")
		
		With SprdView4B
			.MaxRows = .MaxRows + 1
			.Row = .MaxRows
			.Action = SS_ACTION_INSERT_ROW
			
			.Row = .MaxRows
			.Col = 1
			.Text = "TOTAL"
			.Font = VB6.FontChangeBold(.Font, True)
			
			Call CalcRowTotal(SprdView4B, 3, 1, 3, .MaxRows - 1, .MaxRows, 3)
			
		End With
		ShowDetail4 = True
		Exit Function
ErrPart1: 
		ShowDetail4 = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail5(ByRef pSprdView As Object, ByRef pSectionCode As Integer, ByRef mCTYPE As String, ByRef ADataSource As VB6.ADODC) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RS As ADODB.Recordset
		Dim mTotAmountPaid As Double
		Dim mTotTDSAmount As Double
		
		If lblFormType.Text = "26C" Then
			SqlStr = "Select PANNO,PARTYNAME,'',CTYPE,AMOUNTPAID,AMOUNTPAID,VDATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		Else
			SqlStr = "Select PANNO,PARTYNAME,'',AMOUNTPAID,VDATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		End If
		
		SqlStr = SqlStr & vbCrLf & " FROM TDS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SECTIONCODE=" & pSectionCode & " AND CHALLANMKEY IS NOT NULL " & vbCrLf & " AND CANCELLED='N'  " & vbCrLf & " AND CTYPE='" & mCTYPE & "'"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, ADataSource, StrConn, "Y")
		
		With pSprdView
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.MaxRows = .MaxRows + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Action = SS_ACTION_INSERT_ROW
			
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Col = 3
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Text = "TOTAL"
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.FontBold. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.FontBold = True
			
			If lblFormType.Text = "26C" Then
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 6, 1, 6, .MaxRows - 1, .MaxRows, 6)
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 8, 1, 8, .MaxRows - 1, .MaxRows, 8)
			Else
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 4, 1, 4, .MaxRows - 1, .MaxRows, 4)
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 6, 1, 6, .MaxRows - 1, .MaxRows, 6)
			End If
			
		End With
		ShowDetail5 = True
		Exit Function
ErrPart1: 
		ShowDetail5 = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail6(ByRef pSprdView As Object, ByRef pSectionCode As Integer, ByRef mCTYPE As String, ByRef ADataSource As VB6.ADODC) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RS As ADODB.Recordset
		Dim mTotAmountPaid As Double
		Dim mTotTDSAmount As Double
		
		If lblFormType.Text = "26C" Then
			SqlStr = "Select PANNO,PARTYNAME,'',CTYPE,AMOUNTPAID,AMOUNTPAID,VDATE,TDSRATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		Else
			SqlStr = "Select PANNO,PARTYNAME,'',AMOUNTPAID,VDATE,TDSRATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		End If
		
		SqlStr = SqlStr & vbCrLf & " FROM TDS_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SECTIONCODE=" & pSectionCode & " AND CHALLANMKEY IS NOT NULL " & vbCrLf & " AND CANCELLED='N'  " & vbCrLf & " AND CTYPE='" & mCTYPE & "'"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, ADataSource, StrConn, "Y")
		
		With pSprdView
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.MaxRows = .MaxRows + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Action = SS_ACTION_INSERT_ROW
			
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Col = 3
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Text = "TOTAL"
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.FontBold. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.FontBold = True
			
			If lblFormType.Text = "26C" Then
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 6, 1, 6, .MaxRows - 1, .MaxRows, 6)
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 9, 1, 9, .MaxRows - 1, .MaxRows, 9)
			Else
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 4, 1, 4, .MaxRows - 1, .MaxRows, 4)
				'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Call CalcRowTotal(pSprdView, 7, 1, 7, .MaxRows - 1, .MaxRows, 7)
			End If
		End With
		ShowDetail6 = True
		Exit Function
ErrPart1: 
		ShowDetail6 = False
	End Function
	
	
	Private Function ShowDetail7(ByRef pSprdView As Object, ByRef pSectionCode As Integer, ByRef mCTYPE As String, ByRef ADataSource As VB6.ADODC) As Boolean
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RS As ADODB.Recordset
		Dim mTotAmountPaid As Double
		Dim mTotTDSAmount As Double
		
		'    If lblFormType.Caption = "26C" Then
		'        sqlstr = "Select PANNO,PARTYNAME,'',CTYPE,AMOUNTPAID,AMOUNTPAID,VDATE,TDSRATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		'    Else
		'        sqlstr = "Select PANNO,PARTYNAME,'',AMOUNTPAID,VDATE,TDSRATE,TDSAMOUNT,VDATE,CHALLANDATE,CERTIFICATENO,EXEPTIONCNO "
		'    End If
		'
		'    sqlstr = sqlstr & vbCrLf _
		''        & " FROM TDSTRN " & vbCrLf _
		''        & " WHERE SECTIONCODE=" & pSectionCode & " AND CHALLANMKEY IS NOT NULL " & vbCrLf _
		''        & " AND CANCELLED='N'  " & vbCrLf _
		''        & " AND CTYPE='" & mCType & "'"
		
		'' mainclass.AssignDataInSprd sqlstr, ADataSource, StrConn, "Y"
		
		With pSprdView
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.MaxRows = .MaxRows + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Action. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Action = SS_ACTION_INSERT_ROW
			
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Row = .MaxRows
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Col = 3
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.Text = "TOTAL"
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.FontBold. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			.FontBold = True
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object pSprdView.MaxRows. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Call CalcRowTotal(pSprdView, 4, 1, 4, .MaxRows - 1, .MaxRows, 4)
			
			
		End With
		ShowDetail7 = True
		Exit Function
ErrPart1: 
		ShowDetail7 = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function InsertGridDetail(ByRef mSprd As Object, ByRef mRowNo As Double, ByRef mMaxRow As Integer, ByRef mMaxCol As Integer, Optional ByRef mRowTitle As String = "") As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		Dim mCol0 As String
		Dim mCol1 As String
		Dim mCol2 As String
		Dim mCol3 As String
		Dim mCol4 As String
		Dim mCol5 As String
		Dim mCol6 As String
		Dim mCol7 As String
		Dim mCol8 As String
		Dim mCol9 As String
		Dim mCol10 As String
		Dim mCol11 As String
		Dim mCol12 As String
		Dim mCol13 As String
		Dim mCol15 As String
		
		Dim cntRow As Integer
		
		
		SqlStr = ""
		
		With mSprd
			For cntRow = 0 To mMaxRow
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Row = cntRow
				
				mRowNo = mRowNo + (0.00001 * cntRow)
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 0
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol0 = IIf(cntRow = 0, .Text, IIf(cntRow = mMaxRow, "", cntRow))
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 1
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol1 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 2
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol2 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 3
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol3 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 4
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol4 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 5
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol5 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 6
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol6 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 7
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol7 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 8
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol8 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 9
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol9 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 10
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol10 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 11
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol11 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 12
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol12 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 13
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol13 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 14
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol15 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
InsertPart: 
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14,Field15,Field30) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol2) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol3) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol4) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol5) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol6) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol7) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol8) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol9) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol10) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol11) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol12) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol13) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol0) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol15) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRowTitle) & "' " & vbCrLf & " )"
				
				PubDBCn.Execute(SqlStr)
			Next 
		End With
		
		InsertGridDetail = True
		Exit Function
ERR1: 
		'Resume
		MsgInformation(Err.Description)
		InsertGridDetail = False
	End Function
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function InsertLabel(ByRef lblName As String, ByRef txtName As String, ByRef mRow As Integer) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtName) & "'" & vbCrLf & " )"
		
		PubDBCn.Execute(SqlStr)
		
		InsertLabel = True
		Exit Function
ERR1: 
		'Resume
		MsgInformation(Err.Description)
		InsertLabel = False
	End Function
	Private Sub FillForm26Title()
		On Error GoTo ErrPart1
		
		Select Case lblFormType.Text
			
			Case "26A"
				lbl2C.Text = "   (c) Has address of the person responsible for paying any income by way of interest other than interest on securities changed since submitting the last return"
				
				lbl3A.Text = "3. Details of total amount of interest credited / paid and tax deducted thereon :"
				lbl26A4A.Text = "4. (a) Total amount of interest on which no deduction was made in accordance with the provisions of section 194-A(3) (iii) and section 196"
				lbl26A4B.Text = "    (b) Total amount of interest on which no deduction was made in accordance with the provision of section 194A(3)(i)"
				lbl26A4C.Text = "    (c) Total number of payees to whom interest was paid without deduction of tax in accordance with the provisions of section 194A(3)(i)"
				lbl26A4D.Text = "    (d) Total amount of interest on which no deduction was made in accordance with the provision of section 197A(1A)"
				lbl26A4E.Text = "    (e) Total number of payees to whom interest was paid without deduction of tax in accordance with the provisions of section 197A(1A)"
				lbl4.Text = "5. Details of tax paid to the credit of Central Government"
				lbl4A.Text = "    (a) By or on behalf of Central Government :"
				lbl4B.Text = "    (b By persons responsible for paying other than Central Government :"
				lbl5.Text = "6. Details of interest credited / paid during the financial year and of tax deducted at source at the prescribed rates in force :"
				lbl5A.Text = "    (a) Interest credited / paid to companies :"
				lbl5B.Text = "    (b) Interest credited / paid to persons / payees other than companies :"
				
				lbl6.Text = "7. Details of interest credited / paid during the financial year and of tax deducted at source at a lower rate or no tax deducted in accordance with section 197 :"
				lbl6A.Text = "    (a) Interest credited / paid to companies :"
				lbl6B.Text = "    (b) Interest credited / paid to persons / payees other than companies :"
				
				lbl7.Text = "8. Amount of interest credited / paid without deduction of tax during the financial year on furnishing a declaration under section 197A (1A)."
				
			Case "26B"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26C"
				lbl2C.Text = "   (c) Has address of the person responsible for paying any sum for carrying out any work in pursuance of a contact or sub-contact changed since submitting the last return"
				
				lbl3A.Text = "3. Details of payments made to contractors or sub-contractors and tax deducted thereon"
				lbl4.Text = "4. Details of tax paid to the credit of Central Government :"
				lbl4A.Text = "    (a) By or on behalf of Central Government :"
				lbl4B.Text = "    (b) By persons responsible for paying other than Central Government :"
				lbl5.Text = "5. Details of payment made to contractors or sub-contractors and of tax deducted at source at the prescribed rates in force :"
				lbl5A.Text = "    (a) Details of sums paid to contractors or sub-contractors being companies :"
				lbl5B.Text = "    (b Details of sums paid to contractors or sub-contractors other than companies :"
				lbl6.Text = "6. Details of payments made to contractors or sub-contractors and/0r tax deducted at source at a lower rate or no tax deducted in accordanced with section 194C(4) :"
				lbl6A.Text = "    (a) Details of sums paid to contractors or sub-contractors being companies :"
				lbl6B.Text = "    (b) Details of sums paid to contractors or sub-contractors other than companies :"
			Case "26D"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26E"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26F"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26G"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26H"
				lbl3A.Text = "3. "
				lbl4.Text = "4. "
				lbl4A.Text = "    (a) "
				lbl4B.Text = "    (b) "
				lbl5.Text = "5. "
				lbl5A.Text = "    (a) "
				lbl5B.Text = "    (b "
				lbl6.Text = "6. "
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) "
			Case "26I"
				lbl2C.Text = "   (c) Has address of the person responsible for paying any commission or brokerage referred to in section 194H, changed since submitting the last return"
				
				lbl3A.Text = "3. Details of commission (not being insurance commission) or brokerage credited / paid and tax deducted thereon :"
				lbl4.Text = "4. Details of tax paid to the credit of Central Government :"
				lbl4A.Text = "    (a) By or on behalf of Central Government :"
				lbl4B.Text = "    (b) By persons responsible for paying other than Central Government :"
				lbl5.Text = "5. Details of commission (not being insurance commission) or brokerage referred to in section 194H credited / paid during the year and of tax deducted at source at the prescribed rate in force :"
				lbl5A.Text = "    (a) In the case of companies :"
				lbl5B.Text = "    (b In the case of persons / payees other than companies :"
				lbl6.Text = "6. Details of commission (not being insurance commission) or brokerage referred to in section 194H which has been credited or paid during the year and on which no tax deducted in accordance with the provision to section 194 H :"
				lbl6A.Text = "    (a) "
				lbl6B.Text = "    (b) In the case of persons / payees other than companies :"
			Case "26J"
				lbl2C.Text = "   (c) Has address of the person responsible for paying income by way of rent changed since submitting the last return"
				
				lbl3A.Text = "3. Details of rent credited / paid and tax deducted thereon :"
				lbl4.Text = "4. Details of tax paid to the credit of Central Government :"
				lbl4A.Text = "    (a) By or on behalf of Central Government :"
				lbl4B.Text = "    (b) By persons responsible for paying other than Central Government :"
				lbl5.Text = "5. Details of rent credited / paid during the year and of tax deducted at source at the prescribed rates in force :"
				lbl5A.Text = "    (a) In the case of companies :"
				lbl5B.Text = "    (b In the case of persons / payees other than companies :"
				lbl6.Text = "6. Details of rent credited / paid during the year and of tax deducted at source at a lower rate or no tax deducted in accordance with the provisions of section 197 :"
				lbl6A.Text = "    (a) In the case of companies :"
				lbl6B.Text = "    (b) In the case of persons / payees other than companies :"
			Case "26K"
				lbl2C.Text = "   (c) Has address of the person responsible for paying any sum referred to in section 194J changed since submitting the last return"
				
				lbl3A.Text = "3. Detail of fees for professional or technical services referred to in section 194J credited / paid and tax deducted theron :"
				lbl4.Text = "4. Details of tax paid to the credit of Central Government :"
				lbl4A.Text = "    (a) By or on behalf of Central Government :"
				lbl4B.Text = "    (b) By persons responsible for paying tax other than Central Government :"
				lbl5.Text = "5. Details of fees for professional or technical services referred to in section 194J credited / paid during the year and of tax deducted at source at the prescribed rates in force :"
				lbl5A.Text = "    (a) In the case of companies :"
				lbl5B.Text = "    (b In the case of persons / payees other than companies :"
				lbl6.Text = "6. Details of fees for professional or technical services credited / paid during the year and of tax deducted at source at a lower rate or no tax deducted in accordance with the provisions of section 194J(2) :"
				lbl6A.Text = "    (a) In the case of companies :"
				lbl6B.Text = "    (b) In the case of persons / payees other than companies :"
		End Select
		
		Exit Sub
ErrPart1: 
		MsgBox(Err.Description)
	End Sub
	
	Private Function ShowDosReport(ByRef pPrintMode As String) As Boolean
		On Error GoTo ErrPart
		Dim pFileName As String
		Dim mLineCount As Integer
		Dim mPageCount As Integer
		Dim FilePath As String
		
		pFileName = mPubTDSPath & "\Report.Prn"
		
		FilePath = ""
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePath = Dir(mPubTDSPath, FileAttribute.Directory) ''Dir(pFileName)
		
		If FilePath = "" Then
			Call MkDir(mPubTDSPath)
		End If
		
		Call ShellAndContinue("ATTRIB +A -R " & pFileName)
		FileOpen(1, pFileName, OpenMode.Output)
		mLineCount = 1
		mPageCount = 1
		Call PrintPage1(mLineCount, mPageCount)
		Call PrintLine4(mLineCount, mPageCount)
		Call PrintLine5(mLineCount, mPageCount)
		
		
		'    Call PrintPage3
		'    Call PrintPage4
		'    Call PrintPage5
		'    Call PrintPage6
		FileClose(1)
		
		
		Dim mFP As Boolean
		If pPrintMode = "P" Then
			mFP = Shell(My.Application.Info.DirectoryPath & "\PrintReport.bat", AppWinStyle.NormalFocus)
			If mFP = False Then GoTo ErrPart
			'        Shell App.path & "\PrintReport.bat",vbNormalFocus
		Else
			Shell("ATTRIB +R -A " & pFileName)
			Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
			'App.Path & "\RVIEW.EXE "
		End If
		
		ShowDosReport = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		ShowDosReport = False
		''Resume
		FileClose(1)
	End Function
	
	Private Function PrintPage1(ByRef mLineCount As Integer, ByRef mPageCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim Tab1 As Integer
		Dim Tab2 As Integer
		Dim Tab3 As Integer
		Dim Tab4 As Integer
		Dim Tab5 As Integer
		Dim Tab6 As Integer
		Dim Tab7 As Integer
		Dim cntRow As Integer
		Dim mCntRow As Integer
		Dim mTab1 As String
		Dim mTab2 As String
		Dim mTab3 As String
		Dim mTab4 As String
		Dim mTab5 As String
		Dim mTab6 As String
		Dim mTab7 As String
		
		Tab1 = 0
		Tab2 = 40
		Tab3 = 56
		Tab4 = 72
		Tab5 = 88
		Tab6 = 104
		Tab7 = 120
		
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		
		mString = "FORM NO. 26A"
		PrintLine(1, TAB(0), Chr(14) & mString)
		mLineCount = mLineCount + 1
		
		mString = "(See section 194A and rule 37)"
		PrintLine(1, TAB(0), New String(" ", (mPageWidth - Len(mString)) / 2) & mString)
		mLineCount = mLineCount + 1
		
		mString = "Annual return of deduction of tax from interest other than 'Interest on securities' under section 206 of the "
		PrintLine(1, TAB(0), New String(" ", (mPageWidth - Len(mString)) / 2) & mString)
		mLineCount = mLineCount + 1
		
		mString = "Income-tax Act, 1961, for the year ending 31st March, " & RsCompany.Fields("FYEAR").Value
		PrintLine(1, TAB(0), New String(" ", (mPageWidth - Len(mString)) / 2) & mString)
		mLineCount = mLineCount + 1
		
		mString = "1. (a)   Tax Deduction Account Number "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtTDSNo.Text)
		PrintLine(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (b)   Permanent Account Number "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtPanNo.Text)
		PrintLine(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "2. Details of the person responsible for paying any income by way of interest other than interest on securities"
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (a)   Name / Designation "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtCompanyName.Text)
		PrintLine(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (b)   Address "
		Print(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         Flat/Door/Block Number "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtFlat.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         Name of the Premises/Building "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtBuilding.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         Road/Street/Lane "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtRoad.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         Area/Locality "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtArea.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         Town/City/District "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtTown.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         State "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtState.Text)
		Print(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		mString = "         PinCode "
		Print(1, TAB(0), mString)
		mString = ":"
		Print(1, TAB(Tab2), mString)
		mString = UCase(txtPinCode.Text)
		PrintLine(1, TAB(Tab3), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (c)   Has address of the person resonsible for paying any income by way of interest  Tick  as applicable Yes     No "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         other than 'Interest on securities' changed since submitting the last return. "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "3. Details of total amount of interest credited/paid and tax deducted thereon :"
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, TAB(0), New String("-", mPageWidth))
		mLineCount = mLineCount + 1
		
		mString = "Payee"
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "Gross amount"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "Total Amount of"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "Total Amount of"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "Total amount of tax deducted"
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "of interest"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "interest on"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "interest on"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", mPageWidth - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "paid during"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "which no tax"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "which tax"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "Income-tax"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "Surcharge"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "Total"
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "the year"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "deducted"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "deducted"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = ""
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", Tab6 - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = New String("-", Tab7 - Tab6 - 1)
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = New String("-", mPageWidth - Tab7 - 1)
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(1)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "(2)"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(3)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "(4)"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "(5)"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "(6)"
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", Tab6 - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = New String("-", Tab7 - Tab6 - 1)
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = New String("-", mPageWidth - Tab7 - 1)
		Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mCntRow = 1
		
		With SprdView3
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				mTab1 = Trim(.Text)
				.Col = 2
				mTab2 = Trim(.Text)
				.Col = 3
				mTab3 = Trim(.Text)
				.Col = 4
				mTab4 = Trim(.Text)
				.Col = 5
				mTab5 = Trim(.Text)
				.Col = 6
				mTab6 = Trim(.Text)
				.Col = 7
				mTab7 = Trim(.Text)
				
				Print(1, TAB(Tab1), mTab1)
				mString = "|"
				Print(1, TAB(Tab2), mString)
				Print(1, TAB(Tab2 + 1), New String(" ", Tab3 - Tab2 - 1 - Len(mTab2)) & mTab2)
				mString = "|"
				Print(1, TAB(Tab3), mString)
				Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mTab3)) / 2) & mTab3)
				mString = "|"
				Print(1, TAB(Tab4), mString)
				Print(1, TAB(Tab4 + 1), New String(" ", Tab5 - Tab4 - 1 - Len(mTab4)) & mTab4)
				mString = "|"
				Print(1, TAB(Tab5), mString)
				Print(1, TAB(Tab5 + 1), New String(" ", Tab6 - Tab5 - 1 - Len(mTab5)) & mTab5)
				mString = "|"
				Print(1, TAB(Tab6), mString)
				Print(1, TAB(Tab6 + 1), New String(" ", Tab7 - Tab6 - 1 - Len(mTab6)) & mTab6)
				mString = "|"
				Print(1, TAB(Tab7), mString)
				Print(1, TAB(Tab7 + 1), New String(" ", mPageWidth - Tab7 - 1 - Len(mTab7)) & mTab7)
				mString = "|"
				PrintLine(1, TAB(mPageWidth), mString)
				mLineCount = mLineCount + 1
				
				
				If cntRow = .MaxRows Then
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					mLineCount = mLineCount + 1
				Else
					mString = New String("-", Tab2 - Tab1 - 1)
					Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab2), mString)
					mString = New String("-", Tab3 - Tab2 - 1)
					Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab3), mString)
					mString = New String("-", Tab4 - Tab3 - 1)
					Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab4), mString)
					mString = New String("-", Tab5 - Tab4 - 1)
					Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab5), mString)
					mString = New String("-", Tab6 - Tab5 - 1)
					Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab6), mString)
					mString = New String("-", Tab7 - Tab6 - 1)
					Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab7), mString)
					mString = New String("-", mPageWidth - Tab7 - 1)
					Print(1, TAB(Tab7 + 1), New String(" ", (mPageWidth - Tab7 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					PrintLine(1, TAB(mPageWidth), mString)
					mLineCount = mLineCount + 1
				End If
				mCntRow = mCntRow + 1
				If mLineCount >= 65 Then
					PrintLine(1, " ")
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					PrintLine(1, TAB(Tab6), "Page No. : " & mPageCount)
					PrintLine(1, TAB(Tab7), Chr(12))
					
					mLineCount = 1
					mPageCount = mPageCount + 1
				End If
			Next 
		End With
		
		Do While mLineCount <= 65
			PrintLine(1, " ")
			mLineCount = mLineCount + 1
		Loop 
		
		PrintLine(1, TAB(0), New String("-", mPageWidth))
		PrintLine(1, TAB(Tab6), "Page No. : " & mPageCount)
		PrintLine(1, TAB(Tab7), Chr(12))
		mLineCount = 1
		mPageCount = mPageCount + 1
		PrintPage1 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintPage1 = False
		'    Resume
	End Function
	Private Function PrintLine4(ByRef mLineCount As Integer, ByRef mPageCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim Tab1 As Integer
		Dim Tab2 As Integer
		Dim Tab3 As Integer
		Dim Tab4 As Integer
		Dim Tab5 As Integer
		
		Dim cntRow As Integer
		Dim mCntRow As Integer
		Dim mTab1 As String
		Dim mTab2 As String
		Dim mTab3 As String
		Dim mTab4 As String
		Dim mTab5 As String
		
		
		Tab1 = 0
		Tab2 = 10
		Tab3 = 30
		Tab4 = 50
		Tab5 = 70
		
		
		
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		
		mString = "4. (a)   Total amount of interest on which no deduction was made in accordance with the provisions of "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         section 194A(3) (iii) and section 196 "
		Print(1, TAB(0), mString)
		mString = txt26A4A.Text
		PrintLine(1, TAB(Tab5 + 36), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (b)   Total amount of interest on which no deduction was made in accordance with the provisions of "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         section 194A(3) (i)"
		Print(1, TAB(0), mString)
		mString = txt26A4B.Text
		PrintLine(1, TAB(Tab5 + 36), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (c)   Total number of payees to whom interest was paid without deduction of tax in accordance with "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         the provisions of section 194A(3)(i)"
		Print(1, TAB(0), mString)
		mString = txt26A4C.Text
		PrintLine(1, TAB(Tab5 + 36), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (d)   Total number of interest on which no deduction was made in accordance with the provisions of "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         section 197A(1A)"
		Print(1, TAB(0), mString)
		mString = txt26A4D.Text
		PrintLine(1, TAB(Tab5 + 36), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (e)   Total number of payees to whom interest was paid without deduction of tax in accordance with "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		mString = "         the provisions of section 197A(1A)"
		Print(1, TAB(0), mString)
		mString = txt26A4E.Text
		PrintLine(1, TAB(Tab5 + 36), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "5. Details of tax paid to the credit of Central Government"
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (a) By or on behalf of Central Government"
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		PrintLine(1, TAB(0), New String("-", Tab5 - Tab1 - 1))
		mLineCount = mLineCount + 1
		
		mString = "Sl."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "Amount Deducted"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "Transfer"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "Date of"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(Tab5), mString)
		mLineCount = mLineCount + 1
		
		mString = "No."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "Voucher Number"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "Transfer Voucher"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(Tab5), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(Tab5), mString)
		mLineCount = mLineCount + 1
		
		mString = "(1)"
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(2)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "(3)"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(4)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(Tab5), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(Tab5), mString)
		mLineCount = mLineCount + 1
		
		mCntRow = 1
		
		With SprdView4A
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				mTab1 = Trim(.Text)
				.Col = 2
				mTab2 = Trim(.Text)
				.Col = 3
				mTab3 = Trim(.Text)
				.Col = 4
				mTab4 = Trim(.Text)
				
				Print(1, TAB(Tab1), mTab1)
				mString = "|"
				Print(1, TAB(Tab2), mString)
				Print(1, TAB(Tab2 + 1), New String(" ", Tab3 - Tab2 - 1 - Len(mTab2)) & mTab2)
				mString = "|"
				Print(1, TAB(Tab3), mString)
				Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mTab3)) / 2) & mTab3)
				mString = "|"
				Print(1, TAB(Tab4), mString)
				Print(1, TAB(Tab4 + 1), New String(" ", Tab5 - Tab4 - 1 - Len(mTab4)) & mTab4)
				mString = "|"
				PrintLine(1, TAB(Tab5), mString)
				mLineCount = mLineCount + 1
				
				If cntRow = .MaxRows Then
					PrintLine(1, TAB(0), New String("-", Tab5 - Tab1 - 1))
					mLineCount = mLineCount + 1
				Else
					mString = New String("-", Tab2 - Tab1 - 1)
					Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab2), mString)
					mString = New String("-", Tab3 - Tab2 - 1)
					Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab3), mString)
					mString = New String("-", Tab4 - Tab3 - 1)
					Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab4), mString)
					mString = New String("-", Tab5 - Tab4 - 1)
					Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					PrintLine(1, TAB(Tab5), mString)
					mLineCount = mLineCount + 1
				End If
				mCntRow = mCntRow + 1
				If mLineCount >= 65 Then
					PrintLine(1, " ")
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					PrintLine(1, TAB(mPageWidth - 31), "Page No. : " & mPageCount)
					PrintLine(1, TAB(mPageWidth - 2), Chr(12))
					
					mLineCount = 1
					mPageCount = mPageCount + 1
				End If
			Next 
		End With
		
		mString = "   (b) By persons responsible for paying other than Central Government"
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		PrintLine(1, TAB(0), New String("-", mPageWidth))
		mLineCount = mLineCount + 1
		
		mString = "Sl."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "Challan No."
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "Date of payment"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "Amount of tax paid"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "Date of Transfer Voucher"
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = "No."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(Rs.)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", mPageWidth - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = "(1)"
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(2)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "(3)"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(4)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "(5)"
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", mPageWidth - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mCntRow = 1
		
		With SprdView4B
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				mTab1 = Trim(.Text)
				.Col = 2
				mTab2 = Trim(.Text)
				.Col = 3
				mTab3 = Trim(.Text)
				.Col = 4
				mTab4 = Trim(.Text)
				
				Print(1, TAB(Tab1), IIf(cntRow = .MaxRows, "", cntRow))
				mString = IIf(cntRow = .MaxRows, "", "|")
				Print(1, TAB(Tab2), mString)
				Print(1, TAB(Tab2 + 1), mTab1)
				mString = IIf(cntRow = .MaxRows, "", "|")
				Print(1, TAB(Tab3), mString)
				Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mTab2)) / 2) & mTab2)
				mString = "|"
				Print(1, TAB(Tab4), mString)
				Print(1, TAB(Tab4 + 1), New String(" ", Tab5 - Tab4 - 1 - Len(mTab3)) & mTab3)
				mString = "|"
				Print(1, TAB(Tab5), mString)
				Print(1, TAB(Tab5 + 1), mTab4)
				mString = "|"
				PrintLine(1, TAB(mPageWidth), mString)
				mLineCount = mLineCount + 1
				
				
				If cntRow = .MaxRows Then
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					mLineCount = mLineCount + 1
				Else
					mString = New String("-", Tab2 - Tab1 - 1)
					Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab2), mString)
					mString = New String("-", Tab3 - Tab2 - 1)
					Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab3), mString)
					mString = New String("-", Tab4 - Tab3 - 1)
					Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab4), mString)
					mString = New String("-", Tab5 - Tab4 - 1)
					Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					Print(1, TAB(Tab5), mString)
					mString = New String("-", mPageWidth - Tab5 - 1)
					Print(1, TAB(Tab5 + 1), New String(" ", (mPageWidth - Tab5 - 1 - Len(mString)) / 2) & mString)
					mString = "|"
					PrintLine(1, TAB(mPageWidth), mString)
					mLineCount = mLineCount + 1
				End If
				mCntRow = mCntRow + 1
				If mLineCount >= 65 Then
					PrintLine(1, " ")
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					PrintLine(1, TAB(mPageWidth - 31), "Page No. : " & mPageCount)
					PrintLine(1, TAB(mPageWidth - 2), Chr(12))
					
					mLineCount = 1
					mPageCount = mPageCount + 1
				End If
			Next 
		End With
		
		Do While mLineCount <= 65
			PrintLine(1, " ")
			mLineCount = mLineCount + 1
		Loop 
		
		PrintLine(1, TAB(0), New String("-", mPageWidth))
		PrintLine(1, TAB(mPageWidth - 31), "Page No. : " & mPageCount)
		PrintLine(1, TAB(mPageWidth - 2), Chr(12))
		mLineCount = 1
		mPageCount = mPageCount + 1
		PrintLine4 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintLine4 = False
		'    Resume
	End Function
	
	
	Private Function PrintLine5(ByRef mLineCount As Integer, ByRef mPageCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim Tab1 As Integer
		Dim Tab2 As Integer
		Dim Tab3 As Integer
		Dim Tab4 As Integer
		Dim Tab5 As Integer
		Dim Tab6 As Integer
		Dim Tab7 As Integer
		Dim Tab8 As Integer
		Dim Tab9 As Integer
		Dim Tab10 As Integer
		Dim Tab11 As Integer
		
		Dim cntRow As Integer
		Dim mCntRow As Integer
		Dim mTab1 As String
		Dim mTab2 As String
		Dim mTab3 As String
		Dim mTab4 As String
		Dim mTab5 As String
		Dim mTab6 As String
		Dim mTab7 As String
		Dim mTab8 As String
		Dim mTab9 As String
		Dim mTab10 As String
		Dim mTab11 As String
		
		
		Tab1 = 0
		Tab2 = 4
		Tab3 = 15
		Tab4 = 40
		Tab5 = 55
		Tab6 = 67
		Tab7 = 79
		Tab8 = 91
		Tab9 = 103
		Tab10 = 115
		Tab11 = 125
		
		
		
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		PrintLine(1, TAB(0), " ")
		mLineCount = mLineCount + 1
		
		mString = "6. Detail of interest credited/paid during the financial year and of tax deducted at source at the prescribed rates in force : - "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		mString = "   (a)   Interest credited/paid to companies "
		PrintLine(1, TAB(0), mString)
		mLineCount = mLineCount + 1
		
		PrintLine(1, " ")
		mLineCount = mLineCount + 1
		
		PrintLine(1, TAB(0), New String("-", mPageWidth - 1))
		mLineCount = mLineCount + 1
		
		mString = "Sl."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "Permanent"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "Name of"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "Address of"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "Amount of"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "Date on"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "Amount of"
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = "Date on"
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "Date on"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = "Tax"
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "Date of"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = "No."
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "A/c Number"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "company"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "company"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "interest"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "which"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "tax"
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = "which tax"
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "which tax"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = "Deduction"
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "furnishin"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(PAN)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "interest"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "deducted"
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = "deducted"
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "was paid"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = "Certifi."
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "of Tax"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "Credited"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = ""
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "to the"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = "Number"
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "Deduction"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "or paid,"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = ""
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "credit of"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = ""
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "Certific."
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "Whichever"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = ""
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "Central"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = ""
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "to the"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "is"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = ""
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "Govern."
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = ""
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "company"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = ""
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = ""
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = ""
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = ""
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = ""
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "earlier"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = ""
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = ""
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = ""
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = ""
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = ""
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", Tab6 - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = New String("-", Tab7 - Tab6 - 1)
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = New String("-", Tab8 - Tab7 - 1)
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = New String("-", Tab9 - Tab8 - 1)
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = New String("-", Tab10 - Tab9 - 1)
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = New String("-", Tab11 - Tab10 - 1)
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = New String("-", mPageWidth - Tab11 - 1)
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = "(1)"
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = "(2)"
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = "(3)"
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = "(4)"
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = "(5)"
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = "(6)"
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = "(7)"
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = "(8)"
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = "(9)"
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = "(10)"
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = "(11)"
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		mString = New String("-", Tab2 - Tab1 - 1)
		Print(1, TAB(Tab1), New String(" ", (Tab2 - Tab1 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab2), mString)
		mString = New String("-", Tab3 - Tab2 - 1)
		Print(1, TAB(Tab2 + 1), New String(" ", (Tab3 - Tab2 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab3), mString)
		mString = New String("-", Tab4 - Tab3 - 1)
		Print(1, TAB(Tab3 + 1), New String(" ", (Tab4 - Tab3 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab4), mString)
		mString = New String("-", Tab5 - Tab4 - 1)
		Print(1, TAB(Tab4 + 1), New String(" ", (Tab5 - Tab4 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab5), mString)
		mString = New String("-", Tab6 - Tab5 - 1)
		Print(1, TAB(Tab5 + 1), New String(" ", (Tab6 - Tab5 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab6), mString)
		mString = New String("-", Tab7 - Tab6 - 1)
		Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab7), mString)
		mString = New String("-", Tab8 - Tab7 - 1)
		Print(1, TAB(Tab7 + 1), New String(" ", (Tab8 - Tab7 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab8), mString)
		mString = New String("-", Tab9 - Tab8 - 1)
		Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab9), mString)
		mString = New String("-", Tab10 - Tab9 - 1)
		Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab10), mString)
		mString = New String("-", Tab11 - Tab10 - 1)
		Print(1, TAB(Tab10 + 1), New String(" ", (Tab11 - Tab10 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		Print(1, TAB(Tab11), mString)
		mString = New String("-", mPageWidth - Tab11 - 1)
		Print(1, TAB(Tab11 + 1), New String(" ", (mPageWidth - Tab11 - 1 - Len(mString)) / 2) & mString)
		mString = "|"
		PrintLine(1, TAB(mPageWidth), mString)
		mLineCount = mLineCount + 1
		
		
		mCntRow = 1
		
		With SprdView5A
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				mTab1 = Trim(.Text)
				.Col = 2
				mTab2 = Trim(.Text)
				.Col = 3
				mTab3 = Trim(.Text)
				.Col = 4
				mTab4 = Trim(.Text)
				.Col = 5
				mTab5 = Trim(.Text)
				.Col = 6
				mTab6 = Trim(.Text)
				.Col = 7
				mTab7 = Trim(.Text)
				.Col = 8
				mTab8 = Trim(.Text)
				.Col = 9
				mTab9 = VB.Right(Trim(.Text), 6)
				.Col = 10
				mTab10 = Trim(.Text)
				''String((Tab4 - Tab3 - 1 - Len(mTab2)) / 2, " ") &
				''String(Tab5 - Tab4 - 1 - Len(mTab3), " ") &
				Print(1, TAB(Tab1), IIf(cntRow = .MaxRows, "", cntRow))
				mString = IIf(cntRow = .MaxRows, "", "|")
				Print(1, TAB(Tab2), mString)
				Print(1, TAB(Tab2 + 1), mTab1)
				mString = IIf(cntRow = .MaxRows, "", "|")
				Print(1, TAB(Tab3), mString)
				Print(1, TAB(Tab3 + 1), mTab2)
				mString = "|"
				Print(1, TAB(Tab4), mString)
				Print(1, TAB(Tab4 + 1), mTab3)
				mString = "|"
				Print(1, TAB(Tab5), mString)
				Print(1, TAB(Tab5 + 1), New String(" ", Tab6 - Tab5 - 1 - Len(mTab4)) & mTab4)
				mString = "|"
				Print(1, TAB(Tab6), mString)
				Print(1, TAB(Tab6 + 1), New String(" ", (Tab7 - Tab6 - 1 - Len(mTab5)) / 2) & mTab5)
				mString = "|"
				Print(1, TAB(Tab7), mString)
				Print(1, TAB(Tab7 + 1), New String(" ", Tab8 - Tab7 - 1 - Len(mTab6)) & mTab6)
				mString = "|"
				Print(1, TAB(Tab8), mString)
				
				Print(1, TAB(Tab8 + 1), New String(" ", (Tab9 - Tab8 - 1 - Len(mTab7)) / 2) & mTab7)
				mString = "|"
				Print(1, TAB(Tab9), mString)
				
				Print(1, TAB(Tab9 + 1), New String(" ", (Tab10 - Tab9 - 1 - Len(mTab8)) / 2) & mTab8)
				mString = "|"
				Print(1, TAB(Tab10), mString)
				
				Print(1, TAB(Tab10 + 1), mTab9)
				mString = "|"
				Print(1, TAB(Tab11), mString)
				
				Print(1, TAB(Tab11 + 1), mTab10)
				mString = "|"
				PrintLine(1, TAB(mPageWidth), mString)
				mLineCount = mLineCount + 1
				
				If cntRow = .MaxRows Then
					PrintLine(1, TAB(0), New String("-", mPageWidth - 1))
					mLineCount = mLineCount + 1
					'            Else
					'                mString = String(Tab2 - Tab1 - 1, "-")
					'                Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab2); mString;
					'                mString = String(Tab3 - Tab2 - 1, "-")
					'                Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab3); mString;
					'                mString = String(Tab4 - Tab3 - 1, "-")
					'                Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab4); mString;
					'                mString = String(Tab5 - Tab4 - 1, "-")
					'                Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab5); mString;
					'                mString = String(Tab6 - Tab5 - 1, "-")
					'                Print #1, Tab(Tab5 + 1); String((Tab6 - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab6); mString;
					'                mString = String(Tab7 - Tab6 - 1, "-")
					'                Print #1, Tab(Tab6 + 1); String((Tab7 - Tab6 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab7); mString;
					'                mString = String(Tab8 - Tab7 - 1, "-")
					'                Print #1, Tab(Tab7 + 1); String((Tab8 - Tab7 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab8); mString;
					'                mString = String(Tab9 - Tab8 - 1, "-")
					'                Print #1, Tab(Tab8 + 1); String((Tab9 - Tab8 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab9); mString;
					'                mString = String(Tab10 - Tab9 - 1, "-")
					'                Print #1, Tab(Tab9 + 1); String((Tab10 - Tab9 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab10); mString;
					'                mString = String(Tab11 - Tab10 - 1, "-")
					'                Print #1, Tab(Tab10 + 1); String((Tab11 - Tab10 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(Tab11); mString;
					'                mString = String(mPageWidth - Tab11 - 1, "-")
					'                Print #1, Tab(Tab11 + 1); String((mPageWidth - Tab11 - 1 - Len(mString)) / 2, " ") & mString;
					'                mString = "|"
					'                Print #1, Tab(mPageWidth); mString
					'                mLineCount = mLineCount + 1
				End If
				mCntRow = mCntRow + 1
				If mLineCount >= 65 Then
					PrintLine(1, " ")
					PrintLine(1, TAB(0), New String("-", mPageWidth))
					PrintLine(1, TAB(mPageWidth - 31), "Page No. : " & mPageCount)
					PrintLine(1, TAB(mPageWidth - 2), Chr(12))
					
					mLineCount = 1
					mPageCount = mPageCount + 1
				End If
			Next 
		End With
		'
		'    mString = "   (b) By persons responsible for paying other than Central Government"
		'    Print #1, Tab(0); mString
		'    mLineCount = mLineCount + 1
		'
		'    Print #1, " "
		'    mLineCount = mLineCount + 1
		'
		'    Print #1, Tab(0); String(mPageWidth, "-")
		'    mLineCount = mLineCount + 1
		'
		'    mString = "Sl."
		'    Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab2); mString;
		'    mString = "Challan No."
		'    Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab3); mString;
		'    mString = "Date of payment"
		'    Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab4); mString;
		'    mString = "Amount of tax paid"
		'    Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab5); mString;
		'    mString = "Date of Transfer Voucher"
		'    Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(mPageWidth); mString
		'    mLineCount = mLineCount + 1
		'
		'    mString = "No."
		'    Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab2); mString;
		'    mString = ""
		'    Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab3); mString;
		'    mString = ""
		'    Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab4); mString;
		'    mString = "(Rs.)"
		'    Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab5); mString;
		'    mString = ""
		'    Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(mPageWidth); mString
		'    mLineCount = mLineCount + 1
		'
		'    mString = String(Tab2 - Tab1 - 1, "-")
		'    Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab2); mString;
		'    mString = String(Tab3 - Tab2 - 1, "-")
		'    Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab3); mString;
		'    mString = String(Tab4 - Tab3 - 1, "-")
		'    Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab4); mString;
		'    mString = String(Tab5 - Tab4 - 1, "-")
		'    Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab5); mString;
		'    mString = String(mPageWidth - Tab5 - 1, "-")
		'    Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(mPageWidth); mString
		'    mLineCount = mLineCount + 1
		'
		'    mString = "(1)"
		'    Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab2); mString;
		'    mString = "(2)"
		'    Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab3); mString;
		'    mString = "(3)"
		'    Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab4); mString;
		'    mString = "(4)"
		'    Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab5); mString;
		'    mString = "(5)"
		'    Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(mPageWidth); mString
		'    mLineCount = mLineCount + 1
		'
		'    mString = String(Tab2 - Tab1 - 1, "-")
		'    Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab2); mString;
		'    mString = String(Tab3 - Tab2 - 1, "-")
		'    Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab3); mString;
		'    mString = String(Tab4 - Tab3 - 1, "-")
		'    Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab4); mString;
		'    mString = String(Tab5 - Tab4 - 1, "-")
		'    Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(Tab5); mString;
		'    mString = String(mPageWidth - Tab5 - 1, "-")
		'    Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'    mString = "|"
		'    Print #1, Tab(mPageWidth); mString
		'    mLineCount = mLineCount + 1
		'
		'    mCntRow = 1
		'
		'    With SprdView4B
		'        For CntRow = 1 To .MaxRows
		'            .Row = CntRow
		'            .Col = 1
		'            mTab1 = Trim(.Text)
		'            .Col = 2
		'            mTab2 = Trim(.Text)
		'            .Col = 3
		'            mTab3 = Trim(.Text)
		'            .Col = 4
		'            mTab4 = Trim(.Text)
		'
		'            Print #1, Tab(Tab1); IIf(CntRow = .MaxRows, "", CntRow);
		'            mString = IIf(CntRow = .MaxRows, "", "|")
		'            Print #1, Tab(Tab2); mString;
		'            Print #1, Tab(Tab2 + 1); mTab1;
		'            mString = IIf(CntRow = .MaxRows, "", "|")
		'            Print #1, Tab(Tab3); mString;
		'            Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mTab2)) / 2, " ") & mTab2;
		'            mString = "|"
		'            Print #1, Tab(Tab4); mString;
		'            Print #1, Tab(Tab4 + 1); String(Tab5 - Tab4 - 1 - Len(mTab3), " ") & mTab3;
		'            mString = "|"
		'            Print #1, Tab(Tab5); mString;
		'            Print #1, Tab(Tab5 + 1); mTab4;
		'            mString = "|"
		'            Print #1, Tab(mPageWidth); mString
		'            mLineCount = mLineCount + 1
		'
		'
		'            If CntRow = .MaxRows Then
		'                Print #1, Tab(0); String(mPageWidth, "-")
		'                mLineCount = mLineCount + 1
		'            Else
		'                mString = String(Tab2 - Tab1 - 1, "-")
		'                Print #1, Tab(Tab1); String((Tab2 - Tab1 - 1 - Len(mString)) / 2, " ") & mString;
		'                mString = "|"
		'                Print #1, Tab(Tab2); mString;
		'                mString = String(Tab3 - Tab2 - 1, "-")
		'                Print #1, Tab(Tab2 + 1); String((Tab3 - Tab2 - 1 - Len(mString)) / 2, " ") & mString;
		'                mString = "|"
		'                Print #1, Tab(Tab3); mString;
		'                mString = String(Tab4 - Tab3 - 1, "-")
		'                Print #1, Tab(Tab3 + 1); String((Tab4 - Tab3 - 1 - Len(mString)) / 2, " ") & mString;
		'                mString = "|"
		'                Print #1, Tab(Tab4); mString;
		'                mString = String(Tab5 - Tab4 - 1, "-")
		'                Print #1, Tab(Tab4 + 1); String((Tab5 - Tab4 - 1 - Len(mString)) / 2, " ") & mString;
		'                mString = "|"
		'                Print #1, Tab(Tab5); mString;
		'                mString = String(mPageWidth - Tab5 - 1, "-")
		'                Print #1, Tab(Tab5 + 1); String((mPageWidth - Tab5 - 1 - Len(mString)) / 2, " ") & mString;
		'                mString = "|"
		'                Print #1, Tab(mPageWidth); mString
		'                mLineCount = mLineCount + 1
		'            End If
		'            mCntRow = mCntRow + 1
		'            If mLineCount >= 65 Then
		'                Print #1, " "
		'                Print #1, Tab(0); String(mPageWidth, "-")
		'                Print #1, Tab(mPageWidth - 31); "Page No. : " & mPageCount
		'                Print #1, Tab(mPageWidth - 2); Chr(12)
		'
		'                mLineCount = 1
		'                mPageCount = mPageCount + 1
		'            End If
		'        Next
		'    End With
		
		Do While mLineCount <= 65
			PrintLine(1, " ")
			mLineCount = mLineCount + 1
		Loop 
		
		PrintLine(1, TAB(0), New String("-", mPageWidth))
		PrintLine(1, TAB(mPageWidth - 31), "Page No. : " & mPageCount)
		PrintLine(1, TAB(mPageWidth - 2), Chr(12))
		mLineCount = 1
		mPageCount = mPageCount + 1
		PrintLine5 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintLine5 = False
		'    Resume
	End Function
End Class