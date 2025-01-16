Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLeaveSummaryCPLEmp
	Inherits System.Windows.Forms.Form
	
	Dim SqlStr As String
	Dim ADDMode As Boolean
	Dim MODIFYMode As Boolean
	Dim XRIGHT As String
	
	Dim FormActive As Boolean
	Private Const ConRowHeight As Short = 12
	
	Private Const ColSNO As Short = 0
	Private Const ColCode As Short = 1
	Private Const ColName As Short = 2
	Private Const ColCPLEarn As Short = 3
	Private Const ColCPLAVAIL As Short = 4
	Private Const ColCPLAGT As Short = 5
	Private Const ColBalance As Short = 6
	
	Dim CurrFormWidth As Integer
	Dim CurrFormHeight As Integer
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprd(ByRef mRow As Integer)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim cntCol As Integer
		
		With sprdLeave
			.MaxCols = ColBalance
			.Row = mRow
			.set_RowHeight(0, ConRowHeight * 2)
			
			.set_RowHeight(-1, ConRowHeight * 2.3)
			
			.set_ColWidth(ColSNO, 5)
			
			.Col = ColCode
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeEditMultiLine = True
			.set_ColWidth(ColCode, 6)
			
			.Col = ColName
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeEditMultiLine = True
			.set_ColWidth(ColName, 20)
			
			For cntCol = ColCPLEarn To ColCPLAGT
				.Col = cntCol
				.CellType = SS_CELL_TYPE_EDIT
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
				.TypeEditMultiLine = True
				.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
				.set_ColWidth(cntCol, 17)
			Next 
			
			.Col = ColBalance
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeEditMultiLine = True
			.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
			.set_ColWidth(ColBalance, 6)
		End With
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ProtectCell(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols)
		sprdLeave.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetSpreadColor(sprdLeave, mRow)
		Exit Sub
ERR1: 
		If Err.Number = -2147418113 Then Resume Next
		MsgBox(Err.Description, MsgBoxStyle.Information)
	End Sub
	
	Private Function FieldsVarification() As Boolean
		On Error GoTo ERR1
		
		FieldsVarification = True
		
		
		If Trim(txtEmpCode.Text) = "" And chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			MsgInformation("Please Enter the Emp Code.")
			txtEmpCode.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Not IsDate(txtFrom.Text) Then
			MsgInformation("From Date cann't be blank.")
			txtFrom.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		If Not IsDate(txtTo.Text) Then
			MsgInformation("To Date cann't be blank.")
			txtTo.Focus()
			FieldsVarification = False
			Exit Function
		End If
		
		Exit Function
ERR1: 
		MsgInformation(Err.Description)
		FieldsVarification = False
		'    Resume
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FillHeading()
		Dim MainClass_Renamed As Object
		Dim RsTemp As ADODB.Recordset
		Dim cntCol As Integer
		Dim mAddDeduct As Integer
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(sprdLeave)
		
		With sprdLeave
			.MaxCols = ColBalance
			.Row = 0
			
			.Col = ColSNO
			.Text = "S. No."
			
			.Col = ColCode
			.Text = "Emp Code"
			
			.Col = ColName
			.Text = "Emp Name"
			
			.Col = ColCPLEarn
			.Text = "CPL Earn"
			
			.Col = ColCPLAVAIL
			.Text = "CPL Availed"
			
			.Col = ColCPLAGT
			.Text = "CPL Lapsed on Date"
			
			.Col = ColBalance
			.Text = "CPL Balance"
			
		End With
	End Sub
	
	'UPGRADE_WARNING: Event chkALL.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkALL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkALL.CheckStateChanged
		txtEmpCode.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		
	End Sub
	
	Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
		Me.Close()
	End Sub
	
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)
		On Error GoTo ERR1
		Dim SqlStr As String
		Dim mTitle As String
		Dim mSubTitle As String
		
		PubDBCn.Errors.Clear()
		
		'''''Insert Data from Grid to PrintDummyData Table...
		
		If FillPrintDummyData(sprdLeave, 1, sprdLeave.MaxRows, 1, sprdLeave.MaxCols, PubDBCn) = False Then GoTo ERR1
		
		'''''Select Record for print...
		
		SqlStr = ""
		
		SqlStr = FetchRecordForReport(SqlStr)
		
		mTitle = "CPL Summary (Employee Wise)"
		If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
			mSubTitle = "[ From " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & " ]"
		Else
			mSubTitle = "Emp :  " & txtEmpCode.Text & "  " & TxtName.Text & "  [ From " & VB6.Format(txtFrom.Text, "DD/MM/YYYY") & " To " & VB6.Format(txtTo.Text, "DD/MM/YYYY") & " ]"
		End If
		
		Call ShowReport(SqlStr, "LeaveSummaryEmp.Rpt", Mode, mTitle, mSubTitle)
		
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
		If Err.Number = 32755 Or Err.Number = 20507 Then
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		'Resume
	End Sub
	
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		Report1.Action = 1
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
		Dim MainClass_Renamed As Object
		If FieldsVarification = False Then
			Exit Sub
		End If
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(sprdLeave)
		RefreshScreen()
		Call FormatSprd(-1)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmLeaveSummaryCPLEmp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.STRMenuRight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RightsToButton. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.RightsToButton(Me, XRIGHT)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetControlsColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetControlsColor(Me)
		ADDMode = False
		MODIFYMode = False
		
		CurrFormHeight = 7245
		CurrFormWidth = 11355
		
		Me.Top = 0
		Me.Left = 0
		Me.Height = VB6.TwipsToPixelsY(7245)
		Me.Width = VB6.TwipsToPixelsX(11355)
		
		FillHeading()
		
		txtFrom.Text = "01/01/" & VB6.Format(RunDate, "YYYY")
		txtTo.Text = "31/12/" & VB6.Format(RunDate, "YYYY")
		
		optShow(1).Checked = True
		lblFrom.Visible = False
		lblTo.Text = "As on :"
		txtFrom.Visible = False
		txtFrom.Enabled = False
		
		FormatSprd(-1)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub RefreshScreen()
		Dim MainClass_Renamed As Object
		On Error GoTo refreshErrPart
		Dim RsAttn As ADODB.Recordset
		Dim mCode As String
		Dim mDOJ As String
		Dim mDOL As String
		Dim cntRow As Integer
		Dim mName As String
		
		SqlStr = " SELECT EMP_NAME, EMP_CODE, " & vbCrLf & " EMP_DOJ, EMP_LEAVE_DATE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		
		If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "
        Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = SqlStr & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "'"
		End If
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_CODE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)
		
		If RsAttn.EOF = True Then
			Exit Sub
		End If
		
		cntRow = 1
		
		With sprdLeave
			Do While RsAttn.EOF = False
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCode = IIf(IsDbNull(RsAttn.Fields("EMP_CODE").Value), "", RsAttn.Fields("EMP_CODE").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mName = IIf(IsDbNull(RsAttn.Fields("EMP_NAME").Value), "", RsAttn.Fields("EMP_NAME").Value)
				'            mOpening = GetOpeningCPL(mCode)
				
				If optShow(1).Checked = True Then
					Call CalcSummaryLeaves(mCode, mName, cntRow)
				Else
					Call CalcDetailLeaves(mCode, mName, cntRow)
				End If
				cntRow = cntRow + 1
				RsAttn.MoveNext()
			Loop 
		End With
		
		Exit Sub
refreshErrPart: 
		MsgBox(Err.Description)
		'    Resume
	End Sub
	
	
	Private Function CalcSummaryLeaves(ByRef mCode As String, ByRef mName As String, ByRef cntRow As Integer) As Boolean
		On Error GoTo ErrFillLeaves
		Dim RsLeaves As ADODB.Recordset
		Dim SqlStr As String
		Dim mBalance As Double
		Dim mCPLFrom As String
		Dim mOpening As Double
		Dim mCPLEarn As Double
		Dim mCPLAvail As Double
		Dim mEmpType As String
		
		
		
		mCPLEarn = 0
		mCPLAvail = 0
		mOpening = 0
		mBalance = 0
		
		If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
			'UPGRADE_WARNING: Couldn't resolve default property of object MasterNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			mEmpType = IIf(MasterNo = "1", "S", "W")
		End If
		
		If RsCompany.Fields("COMPANY_CODE").Value = 12 And mEmpType = "W" Then
			mCPLFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -180, CDate(VB6.Format(txtTo.Text, "DD/MM/YYYY"))))
		Else
			mCPLFrom = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -120, CDate(VB6.Format(txtTo.Text, "DD/MM/YYYY"))))
		End If
		
		If GetOpeningCPL(mCode, mCPLFrom, mCPLEarn, mCPLAvail, mBalance) = False Then GoTo ErrFillLeaves
		
		With sprdLeave
			
			.MaxRows = cntRow
			.Row = cntRow
			
			.Col = ColCode
			.Text = mCode
			
			.Col = ColName
			.Text = mName
			
			.Col = ColCPLEarn
			.Text = CStr(Val(CStr(mCPLEarn)))
			
			.Col = ColCPLAVAIL
			.Text = CStr(Val(CStr(mCPLAvail)))
			
			.Col = ColCPLAGT
			.Text = ""
			
			.Col = ColBalance
			.Text = CStr(Val(CStr(mBalance)))
		End With
		
		CalcSummaryLeaves = True
		Exit Function
ErrFillLeaves: 
		CalcSummaryLeaves = False
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function CalcDetailLeaves(ByRef mCode As String, ByRef mName As String, ByRef cntRow As Integer) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrFillLeaves
		Dim RsLeaves As ADODB.Recordset
		Dim SqlStr As String
		Dim mBalance As Double
		Dim mEFHalfDate As String
		Dim mESHalfDate As String
		Dim mETHalfDate As String
		Dim mEFOHalfDate As String
		
		Dim mTrans As Boolean
		Dim mDate As String
		Dim mCPLEarn As Double
		Dim mCPLAvail As Double
		Dim mFromDate As String
		Dim mString As String
		Dim mEmpType As String
		
		
		
		mCPLEarn = 0
		mCPLAvail = 0
		mTrans = False
		
		If MainClass.ValidateWithMasterTable(mCode, "EMP_CODE", "EMP_CAT_TYPE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
			'UPGRADE_WARNING: Couldn't resolve default property of object MasterNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			mEmpType = IIf(MasterNo = "1", "S", "W")
		End If
		
		With sprdLeave
			.MaxRows = cntRow
			.Row = cntRow
			
			.Col = ColCode
			.Text = mCode
			
			.Col = ColName
			.Text = mName
		End With
		
		SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
		
		If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf optShow(2).Checked = True Then 
			If RsCompany.Fields("COMPANY_CODE").Value = 12 And mEmpType = "W" Then
				mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -180, CDate(VB6.Format(txtTo.Text, "DD/MM/YYYY"))))
			Else
				mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -120, CDate(VB6.Format(txtTo.Text, "DD/MM/YYYY"))))
			End If
            SqlStr = SqlStr & vbCrLf & " AND ATTN_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ATTN_DATE<=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If
		
		'    SqlStr = SqlStr & vbCrLf & " AND (FIRSTHALF= " & CPLEARN & " OR SECONDHALF= " & CPLEARN & ")"
		
		SqlStr = SqlStr & vbCrLf & " AND CPl_EARN>0"
		
		SqlStr = SqlStr & vbCrLf & "ORDER BY ATTN_DATE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeaves, ADODB.LockTypeEnum.adLockOptimistic)
		If RsLeaves.EOF = False Then
			Do While Not RsLeaves.EOF
				mEFHalfDate = ""
				mESHalfDate = ""
				mETHalfDate = ""
				mEFOHalfDate = ""
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mDate = IIf(IsDbNull(RsLeaves.Fields("ATTN_DATE").Value), "", RsLeaves.Fields("ATTN_DATE").Value)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				mCPLEarn = (IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) * 0.5)
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) = 1 Then
					mEFHalfDate = mDate
					mESHalfDate = ""
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				ElseIf IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) = 2 Then 
					mEFHalfDate = mDate
					mESHalfDate = mDate
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				ElseIf IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) = 3 Then 
					mEFHalfDate = mDate
					mESHalfDate = mDate
					mETHalfDate = mDate
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				ElseIf IIf(IsDbNull(RsLeaves.Fields("CPL_EARN").Value), 0, RsLeaves.Fields("CPL_EARN").Value) = 4 Then 
					mEFHalfDate = mDate
					mESHalfDate = mDate
					mETHalfDate = mDate
					mEFOHalfDate = mDate
				End If
				
				If mCPLEarn > 0 Then
					With sprdLeave
						
						.MaxRows = cntRow
						.Row = cntRow
						
						.Col = ColCPLEarn
						'                    .Text = mEFHalfDate & IIf(mEFHalfDate = "", "", IIf(mESHalfDate = "", "", ", ")) & mESHalfDate
						mString = mEFHalfDate
						mString = mEFHalfDate & IIf(mEFHalfDate = "", "", IIf(mESHalfDate = "", "", ", ")) & mESHalfDate
						mString = mString & IIf(mString = "", "", IIf(mETHalfDate = "", "", ", ")) & mETHalfDate
						mString = mString & IIf(mString = "", "", IIf(mEFOHalfDate = "", "", ", ")) & mEFOHalfDate
						.Text = mString
						
						.Col = ColCPLAVAIL
						.Text = GetCPLAvailDate(mCode, mDate)
						
						.Col = ColCPLAGT
						.Text = VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 120, CDate(mDate)), "DD/MM/YYYY")
						
						cntRow = cntRow + 1
					End With
					mTrans = True
				End If
				RsLeaves.MoveNext()
			Loop 
		End If
		
		'    With sprdLeave
		'        .MaxRows = cntRow - IIf(mTrans = True, 1, 0)
		'        .Row = cntRow - IIf(mTrans = True, 1, 0)
		'
		'        .Col = ColBalance
		'        .Text = Val(mBalance)
		'    End With
		
		CalcDetailLeaves = True
		Exit Function
ErrFillLeaves: 
		CalcDetailLeaves = False
	End Function
	'UPGRADE_WARNING: Event frmLeaveSummaryCPLEmp.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmLeaveSummaryCPLEmp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		On Error GoTo ErrPart
		Dim mReFormWidth As Integer
		
		mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
		
		sprdLeave.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
		
		Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
		CurrFormWidth = mReFormWidth
		
		'    MainClass.SetSpreadColor SprdMain, -1
		'    MainClass.SetSpreadColor SprdOption, -1
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	'UPGRADE_WARNING: Event optShow.CheckedChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShow.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = optShow.GetIndex(eventSender)
			If Index = 0 Then
				lblFrom.Visible = True
				lblTo.Text = "To :"
				txtFrom.Visible = True
				txtFrom.Enabled = True
			Else
				lblFrom.Visible = False
				lblTo.Text = "As on :"
				txtFrom.Visible = False
				txtFrom.Enabled = False
			End If
		End If
	End Sub
	
	Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
		txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")
		If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
			MsgInformation("Invalid Employee Code ")
			Cancel = True
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MasterNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			TxtName.Text = MasterNo
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE",  ,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
			txtEmpCode.Text = AcName1
			TxtName.Text = AcName
		End If
	End Sub
End Class