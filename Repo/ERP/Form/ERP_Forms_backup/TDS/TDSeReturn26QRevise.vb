Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTDSeReturn26QRevise
	Inherits System.Windows.Forms.Form
	Dim XRIGHT As String
	'Dim PvtDBCn As ADODB.Connection
	
	Private Const RowHeight As Short = 15
	
	Dim mActiveRow As Integer
	Dim FormActive As Boolean
	Private Const mPageWidth As Short = 135
	Private Const mDelimited As String = "^"
	Private Sub PrintStatus(ByRef pPrintEnable As Boolean)
		CmdPreview.Enabled = pPrintEnable
		cmdPrint.Enabled = pPrintEnable
	End Sub
	
	Private Sub cmdCD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCD.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ShowDosReport("V")
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
		Me.Close()
	End Sub
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForTDS(Crystal.DestinationConstants.crptToWindow)
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
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearCRptFormulas(Report1)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		PubDBCn.Execute(SqlStr)
		
		SqlStr = ""
		
		'''''Select Record for print...
		frmPrintTDS.ShowDialog()
		frmPrintTDS.OptFormChallan.Enabled = False
		If G_PrintLedg = False Then
			Exit Sub
		End If
		
		Call InsertIntoPrintDummy()
		
		If frmPrintTDS.OptForm26.Checked = True Then
			If lblFormType.Text = "26Q" Then
				mTitle = "Form No. 26Q"
				mSubTitle = "(See section 193, 194, 194A, 194B, 194BB, 194C, 194D, 194EE, 194F, 194G, 194H, 194I, 194J, 194LA and rule 31A)"
			Else
				mTitle = "Form No. 27Q"
				mSubTitle = "(See section 193, 194, 194A, 194B, 194BB, 194C, 194D, 194EE, 194F, 194G, 194H, 194I, 194J, 194LA and rule 31A)"
			End If
			
			mReportFileName = "TDSeReturn26Q.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr, 1)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 1)
		ElseIf frmPrintTDS.OptForm27A.Checked = True Then 
			
			mTitle = "Form No. 27A"
			mSubTitle = "[See rule 37B"
			
			mReportFileName = "TDSeReturn27AQ.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr, 3)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 3)
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			mTitle = "ANNEXURE - DEDUCTEE WISE BREAK-UP OF TDS"
			mSubTitle = ""
			
			mReportFileName = "TDSeReturn26QAnnx.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr, 1)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 2)
		End If
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearCRptFormulas(Report1)
		
		PrintStatus = True
		frmPrintTDS.Close()
		Exit Sub
ERR1: 
		If Err.Number = 32755 Then
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		Else
			MsgInformation(Err.Description)
		End If
		frmPrintTDS.Close()
	End Sub
	Private Sub InsertIntoPrintDummy()
		On Error GoTo ERR1
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		If frmPrintTDS.OptForm26.Checked = True Then
			If InsertGridDetail(SprdView26, 1, (SprdView26.MaxRows), (SprdView26.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			If InsertGridDetail(SprdViewAnnex, 1, (SprdViewAnnex.MaxRows), (SprdViewAnnex.MaxCols)) = False Then GoTo ERR1
		End If
		
		PubDBCn.CommitTrans()
		Exit Sub
ERR1: 
		'Resume
		PubDBCn.RollbackTrans()
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function InsertGridDetail(ByRef mSprd As Object, ByRef mRowNo As Double, ByRef mMaxRow As Integer, ByRef mMaxCol As Integer) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
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
		Dim mCol14 As String
		Dim mCol15 As String
		Dim mCol16 As String
		Dim mCol17 As String
		Dim mCol18 As String
		Dim mCol19 As String
		Dim mCol20 As String
		Dim mCol21 As String
		Dim mCol22 As String
		Dim mCol23 As String
		Dim mCol24 As String
		Dim mCol25 As String
		Dim mCol26 As String
		Dim mCol27 As String
		Dim mCol28 As String
		
		
		Dim cntRow As Integer
		
		
		SqlStr = ""
		
		With mSprd
			For cntRow = 1 To mMaxRow
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Row. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Row = cntRow
				
				mRowNo = mRowNo + (0.00001 * cntRow)
				
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
				mCol14 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 15
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol15 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 16
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol16 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 17
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol17 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 18
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol18 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 19
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol19 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 20
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol20 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 21
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol21 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 22
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol22 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 23
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol23 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 24
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol24 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 25
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol25 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 26
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol26 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 27
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol27 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 28
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol28 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				
InsertPart: 
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14,Field15,Field16," & vbCrLf & " Field17,Field18,Field19,Field20,Field21,Field22,Field23," & vbCrLf & " Field24,Field25,Field26,Field27,Field28" & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol2) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol3) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol4) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol5) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol6) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol7) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol8) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol9) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol10) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol11) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol12) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol13) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol14) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol15) & "', "
				
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol16) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol17) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol18) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol19) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol20) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol21) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol22) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol23) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol24) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol25) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol26) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol27) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol28) & "' )"
				
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
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mReportNo As Integer)
		Dim MainClass_Renamed As Object
		Dim mFormTitle As String
		Dim mTotAmountPaid As Double
		Dim mTotDeduct As Double
		Dim mTotPerson As Double
		Dim mTotChallanAmount As Double
		Dim mPartyName As String
		Dim mTotAnnexNo As Double
		
		Dim cntRow As Integer
		Dim mTANNo As String
		Dim mPANNo As String
		Dim mFormName As String
		
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mTANNo = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mPANNo = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TANNo=""" & Trim(mTANNo) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PANNo=""" & Trim(mPANNo) & """")
		
		
		If frmPrintTDS.OptForm26.Checked = True Then
			mFormTitle = "Quarterly statement of deduction of tax under sub-section (3) of section 200 of the Income-tax Act, 1961 in respect of payments other than salary for the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY")
		ElseIf frmPrintTDS.OptForm27A.Checked = True Then 
			mFormTitle = "Form for furnishing information with the statement of deduction/ collection of tax at source(tick whichever is applicable) filed on computer media for the period (from " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & " to " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ")"
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			mFormTitle = "Details of amount paid / Credited during the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY") & " and of tax deducted at source"
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & txtFYear.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & Trim(txtAYear.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "STATUS=""" & Trim(txtReturnFiled.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "ProReceiptNo=""" & Trim(txtProvReceiptNo.Text) & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & Trim(txtDeductorType.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "BRANCHNAME=""" & Trim(txtBranch.Text) & """")
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Flat=""" & txtFlat.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Building=""" & Trim(txtBuilding.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Road=""" & Trim(txtRoad.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Area=""" & Trim(txtArea.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Town=""" & txtTown.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "State=""" & Trim(txtState.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PinCode=""" & Trim(txtPinCode.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PhoneNo=""" & Trim(txtPhone.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Email=""" & Trim(txtEmail.Text) & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PersonName=""" & txtPersonName_p.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Flat_P=""" & txtFlat_p.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Building_P=""" & Trim(txtBuilding_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Road_P=""" & Trim(txtRoad_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Area_P=""" & Trim(txtArea_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Town_P=""" & txtTown_p.Text & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "State_P=""" & Trim(txtState_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PinCode_P=""" & Trim(txtPinCode_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PhoneNo_P=""" & Trim(txtPhone_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Email_P=""" & Trim(txtEmail_p.Text) & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AuthName=""" & Trim(txtPersonName_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Designation=""" & Trim(txtDesg.Text) & """")
		
		If frmPrintTDS.OptForm27A.Checked = True Then
			With SprdViewAnnex
				For cntRow = 1 To .MaxRows
					.Row = cntRow
					.Col = 6
					mTotAmountPaid = mTotAmountPaid + Val(.Text)
					
					.Col = 11
					mTotDeduct = mTotDeduct + Val(.Text)
					
					.Col = 4
					mTotPerson = mTotPerson + 1
				Next 
			End With
			
			With SprdView26
				mPartyName = ""
				For cntRow = 1 To .MaxRows
					.Row = cntRow
					mTotAnnexNo = 0
					
					.Col = 8
					mTotChallanAmount = mTotChallanAmount + Val(.Text)
				Next 
			End With
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAmountPaid=""" & VB6.Format(mTotAmountPaid, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotDeduct=""" & VB6.Format(mTotDeduct, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotPerson=""" & mTotPerson & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotChallanAmount=""" & VB6.Format(mTotChallanAmount, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAnnexNo=""" & mTotAnnexNo & """")
			mFormName = UCase(lblFormType.Text)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "FormName=""" & mFormName & """")
			
		End If
		
		
		' Report1.CopiesToPrinter = PrintCopies
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		
		Report1.MarginLeft = 0
		Report1.MarginRight = 0
		
		Report1.Action = 1
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function FetchRecordForReport(ByRef mSqlStr As String, ByRef mReportNo As Integer) As String
		Dim MainClass_Renamed As Object
		Dim mSection As String
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		
		
		If mReportNo = 1 Then
			'        mSqlStr = mSqlStr & " AND FIELD2='CD'"
		ElseIf mReportNo = 2 Then 
			mSqlStr = mSqlStr & " AND FIELD2='DD'"
			
			If frmPrintTDS.chkPrintOption(0).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = "'193'"
			End If
			If frmPrintTDS.chkPrintOption(1).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194'"
			End If
			If frmPrintTDS.chkPrintOption(2).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194A'"
			End If
			If frmPrintTDS.chkPrintOption(3).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194B'"
			End If
			If frmPrintTDS.chkPrintOption(4).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194BB'"
			End If
			If frmPrintTDS.chkPrintOption(5).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194C'"
			End If
			If frmPrintTDS.chkPrintOption(6).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194D'"
			End If
			If frmPrintTDS.chkPrintOption(7).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194EE'"
			End If
			If frmPrintTDS.chkPrintOption(8).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194F'"
			End If
			If frmPrintTDS.chkPrintOption(9).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194G'"
			End If
			If frmPrintTDS.chkPrintOption(10).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194H'"
			End If
			If frmPrintTDS.chkPrintOption(11).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194I'"
			End If
			If frmPrintTDS.chkPrintOption(12).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194J'"
			End If
			If frmPrintTDS.chkPrintOption(13).CheckState = System.Windows.Forms.CheckState.Checked Then
				mSection = IIf(mSection = "", "", mSection & ", ") & "'194K'"
			End If
			
			mSection = "(" & mSection & ")"
			mSqlStr = mSqlStr & " AND FIELD5 IN " & mSection & ""
			
		ElseIf mReportNo = 3 Then 
			mSqlStr = mSqlStr & " AND FIELD2='FH'"
		End If
		
		'    If mReportNo = 2 Then
		'        mSqlStr = mSqlStr & " ORDER BY  SUBROW"     ''FIELD5, FIELD8,
		'    Else
		mSqlStr = mSqlStr & " ORDER BY SUBROW"
		'    End If
		FetchRecordForReport = mSqlStr
		
	End Function
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForTDS(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
		SearchAccounts()
	End Sub
	
	
	
	
	Private Sub cmdValidate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdValidate.Click
		Dim mFP As Boolean
		mFP = Shell(mLocalPath & "\TDS_FVU.bat", AppWinStyle.NormalFocus)
	End Sub
	
	Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
		SearchAccounts()
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub SearchAccounts()
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SearchMaster. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SearchMaster(TxtAccount, "vwFIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'")
		If AcName <> "" Then
			TxtAccount.Text = AcName
		End If
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
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
		If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
	End Sub
	
	
	Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAccount.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		On Error GoTo ErrPart
		
		If Trim(TxtAccount.Text) = "" Then GoTo EventExitSub
		
		'UPGRADE_WARNING: Untranslated statement in txtAccount_Validate. Please check source code.
		
		GoTo EventExitSub
ErrPart: 
		MsgBox(Err.Description, MsgBoxStyle.Information)
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
		If FieldsVerification = False Then Exit Sub
		
		Call PrintStatus(False)
		Call Clear1()
		
		Show1()
		FormatSprdView()
		
		Call PrintStatus(True)
	End Sub
	Function FieldsVerification() As Boolean
		On Error GoTo ERR1
		
		'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.
		If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then FieldsVerification = False : txtDateFrom.Focus()
		'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.
		If FYChk(CStr(CDate(txtDateTo.Text))) = False Then FieldsVerification = False : txtDateTo.Focus()
		
		If Trim(TxtAccount.Text) = "" Then
			MsgInformation("Please Enter Valid TDS Account Name.")
			TxtAccount.Focus()
			FieldsVerification = False
			Exit Function
		End If
		
		'UPGRADE_WARNING: Untranslated statement in FieldsVerification. Please check source code.
		
		If chkRefilling.CheckState = System.Windows.Forms.CheckState.Checked Then
			If cboCorrectionType.SelectedIndex = 0 Then
				MsgInformation("Please Enter Valid Correction Type.")
				cboCorrectionType.Focus()
				FieldsVerification = False
				Exit Function
			End If
		End If
		
		FieldsVerification = True
		Exit Function
ERR1: 
		FieldsVerification = False
	End Function
	'UPGRADE_WARNING: Form event frmTDSeReturn26QRevise.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Public Sub frmTDSeReturn26QRevise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		On Error GoTo ERR1
		If FormActive = True Then Exit Sub
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		Me.Text = "TDS e-Return (Form " & lblFormType.Text & ")"
		Call PrintStatus(False)
		
		cboCorrectionType.Items.Clear()
		'    cboCorrectionType.AddItem "0 : None"
		cboCorrectionType.Items.Add("1 : C1 - DEDUCTOR (EXCLUDING TAN) DETAILS")
		cboCorrectionType.Items.Add("2 : C2 - DEDUCTOR (EXCLUDING TAN), AND/OR CHALLAN DETAILS")
		cboCorrectionType.Items.Add("3 : C3 - DEDUCTOR (EXCLUDING TAN), AND/OR CHALLAN, AND/OR DEDUCTEE DETAILS")
		cboCorrectionType.Items.Add("4 : C5 - PAN UPDATE")
		cboCorrectionType.Items.Add("5 : C9 - ADDITION OF CHALLAN")
		cboCorrectionType.Items.Add("6 : Y - CANCELLATION OF STATEMENT")
		cboCorrectionType.SelectedIndex = 0
		
		
		FormatSprdView()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		FormActive = True
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTDSeReturn26QRevise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
		Me.Height = VB6.TwipsToPixelsY(6285)
		Me.Width = VB6.TwipsToPixelsX(10155)
		SSTab1.SelectedIndex = 0
		
		txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
		txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
		txtTDSAcNo.Enabled = False
		txtPanNo.Enabled = False
		
		FormatSprdView()
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
		
		If ShowDetail26 = False Then GoTo ErrPart
		If ShowDetailAnnex = False Then GoTo ErrPart
		
		
		FormatSprdView()
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgInformation(Err.Description)
		
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetailAnnex() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAddress As String
		Dim mNewAddress As String
		Dim mDeducteeCode As String
		Dim mAddress1 As String
		Dim mAddress2 As String
		Dim mAddress3 As String
		Dim mAddress4 As String
		Dim mTDSAccountCode As String
		Dim mNetAmount As Double
		Dim mChallanWiseSNo As Integer
		Dim mPrevChallanMkey As String
		Dim mChallanSNo As Integer
		Dim mChallanMkey As String
		
		Dim mSectionCode As String
		Dim mBSRCode As String
		Dim mDepositDate As String
		Dim mChallanNo As String
		
		Dim mTotalTDS As Double
		Dim mTotalInerest As Double
		Dim mOtherAmt As Double
		Dim mTotalTaxDeposit As Double
		
		Dim mTDSAmount As Double
		
		
		'UPGRADE_WARNING: Untranslated statement in ShowDetailAnnex. Please check source code.
		
		SqlStr = " Select SECTIONMST.NAME, CMST.PAN_NO PANNO,CMST.SUPP_CUST_NAME,'', " & vbCrLf & " (TRN.TDSAMOUNT*100*.100/112.2) AS SURAMT, " & vbCrLf & " (TRN.TDSAMOUNT*100*.022/112.2) AS CESS,  " & vbCrLf & " (TRN.TDSAMOUNT - ((TRN.TDSAMOUNT*100*.022/112.2)+ (TRN.TDSAMOUNT*100*.100/112.2))) AS TDSAMT," & vbCrLf & " TRN.TDSAMOUNT, TRN.AMOUNTPAID,TRN.VDATE,TRN.TDSRATE, " & vbCrLf & " TRN.CHALLANNO ,TRN.CHALLANDATE, " & vbCrLf & " TRN.CERTIFICATENO, TRN.EXEPTIONCNO,CMST.CTYPE, " & vbCrLf & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, " & vbCrLf & " CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, " & vbCrLf & " CMST.PAN_NO,PRINTDATE,BANKCODE,TRN.COMPANY_CODE,CHALLANMKEY "
		
		SqlStr = SqlStr & vbCrLf & " FROM vwTDS_TRN TRN, vwTDS_SECTION_MST SECTIONMST,vwFIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE "
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND"
		End If
		
		SqlStr = SqlStr & vbCrLf & " TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND TRN.PARTYCODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND TRN.CANCELLED='N' AND ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf & " AND TRN.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.CHALLANMKEY IN ( " & GetChallanQry(True) & ") "
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY  CHALLANMKEY, SECTIONMST.NAME, TRN.COMPANY_CODE,  CMST.SUPP_CUST_NAME, TRN.VDATE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		mChallanWiseSNo = 1
		
		With SprdViewAnnex
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.Row = cntRow
					.Col = 1
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If mPrevChallanMkey = IIf(IsDbNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value) Then
						mChallanWiseSNo = mChallanWiseSNo + 1
					Else
						mChallanWiseSNo = 1
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						mChallanMkey = IIf(IsDbNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)
						mChallanSNo = GetChallanSNO(mChallanMkey, mSectionCode, mBSRCode, mDepositDate, mChallanNo, mTotalTDS, mTotalInerest, mOtherAmt, mTotalTaxDeposit)
					End If
					
					.Text = CStr(mChallanWiseSNo) '''cntRow
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mDeducteeCode = IIf(IsDbNull(RsTemp.Fields("CType").Value), "N", RsTemp.Fields("CType").Value)
					.Text = IIf(mDeducteeCode = "C", "01", "02")
					
					.Col = 3
					
					If Len(RsTemp.Fields("PAN_NO").Value) = 10 Then
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Text = IIf(IsDbNull(RsTemp.Fields("PAN_NO").Value), "", RsTemp.Fields("PAN_NO").Value)
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					ElseIf Trim(RsTemp.Fields("PAN_NO").Value) = "" Or IsDbNull(RsTemp.Fields("PAN_NO").Value) Then 
						.Text = "PANNOTAVBL"
					Else
						.Text = "PANINVALID"
					End If
					
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY") '' Format(IIf(IsNull(RsTemp!CHALLANDATE), "", RsTemp!CHALLANDATE), "DD/MM/YYYY")
					
					.Col = 6
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AmountPaid").Value), "", RsTemp.Fields("AmountPaid").Value), "0.00") '' Format(IIf(IsNull(RsTemp!TDSAMOUNT), "", RsTemp!TDSAMOUNT), "0.00")
					
					
					.Col = 7
					.Text = ""
					
					.Col = 8
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mTDSAmount = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00"))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mTDSAmount = mTDSAmount - CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00"))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mTDSAmount = mTDSAmount - CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00"))
					
					'                .Text = Format(IIf(IsNull(RsTemp!TDSAMT), "", RsTemp!TDSAMT), "0.00")           '''Format(IIf(IsNull(RsTemp!TDSAMOUNT), "", RsTemp!TDSAMOUNT), "0.00")
					.Text = VB6.Format(mTDSAmount, "0.00")
					
					.Col = 9
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00") ''Format(IIf(IsNull(RsTemp!SURCHARGE), "", RsTemp!SURCHARGE), "0.00")
					
					.Col = 10
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00") ''Format(IIf(IsNull(RsTemp!EDU_CESS), "", RsTemp!EDU_CESS), "0.00")
					
					mNetAmount = CDbl(VB6.Format(mTDSAmount, "0.00")) ''Format(IIf(IsNull(RsTemp!TDSAMT), "", RsTemp!TDSAMT), "0.00")            '''IIf(IsNull(RsTemp!TDSAMOUNT), 0, RsTemp!TDSAMOUNT)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mNetAmount = mNetAmount + CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURAMT").Value), "", RsTemp.Fields("SURAMT").Value), "0.00")) ''IIf(IsNull(RsTemp!SURCHARGE), 0, RsTemp!SURCHARGE)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mNetAmount = mNetAmount + CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("CESS").Value), "", RsTemp.Fields("CESS").Value), "0.00")) ''IIf(IsNull(RsTemp!EDU_CESS), 0, RsTemp!EDU_CESS)
					
					.Col = 11
					.Text = VB6.Format(mNetAmount, "0.00")
					
					.Col = 12
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00") '' Format(mNetAmount, "0.00")      '''
					
					.Col = 13
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
					
					.Col = 14
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDSRATE").Value), "", RsTemp.Fields("TDSRATE").Value), "0.0000")
					
					.Col = 15
					.Text = ""
					
					.Col = 16
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
					.Col = 17
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mPrevChallanMkey = IIf(IsDbNull(RsTemp.Fields("CHALLANMKEY").Value), "", RsTemp.Fields("CHALLANMKEY").Value)
					
					.Col = 18
					.Text = Trim(mBSRCode)
					
					.Col = 19
					.Text = Trim(mDepositDate)
					
					.Col = 20
					.Text = Trim(mChallanNo)
					
					.Col = 21
					.Text = Trim(mSectionCode)
					
					.Col = 22
					.Text = VB6.Format(mTotalTDS, "0.00")
					
					.Col = 23
					.Text = VB6.Format(mTotalInerest, "0.00")
					
					.Col = 24
					.Text = VB6.Format(mOtherAmt, "0.00")
					
					.Col = 25
					.Text = VB6.Format(mTotalTaxDeposit, "0.00")
					
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						cntRow = cntRow + 1
						.MaxRows = cntRow
					End If
				Loop 
			End If
		End With
		ShowDetailAnnex = True
		Exit Function
ErrPart1: 
		ShowDetailAnnex = False
		'    Resume
	End Function
	
	Private Function GetChallanSNO(ByRef pChallanMKey As String, ByRef pSectionCode As String, ByRef pBSRCode As String, ByRef pDepositDate As String, ByRef pChallanNo As String, ByRef pTotalTDS As Double, ByRef pTotalInerest As Double, ByRef pOtherAmt As Double, ByRef pTotalTaxDeposit As Double) As Integer
		On Error GoTo ErrPart1
		Dim cntRow As Integer
		
		GetChallanSNO = 0
		With SprdView26
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 15
				If Trim(.Text) = Trim(pChallanMKey) Then
					GetChallanSNO = cntRow
					.Col = 2
					pSectionCode = Trim(.Text)
					
					.Col = 3
					pTotalTDS = Val(.Text)
					
					.Col = 4
					pTotalTDS = pTotalTDS + Val(.Text)
					
					.Col = 5
					pTotalTDS = pTotalTDS + Val(.Text)
					
					.Col = 6
					pTotalInerest = Val(.Text)
					
					.Col = 7
					pOtherAmt = Val(.Text)
					
					.Col = 8
					pTotalTaxDeposit = Val(.Text)
					
					.Col = 10
					pBSRCode = Trim(.Text)
					
					.Col = 11
					pDepositDate = Trim(.Text)
					
					.Col = 12
					pChallanNo = Trim(.Text)
					
					Exit For
				End If
			Next 
		End With
		
		
		
		Exit Function
ErrPart1: 
		
	End Function
	
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail26() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mTDSAccountCode As String
		
		
		'UPGRADE_WARNING: Untranslated statement in ShowDetail26. Please check source code.
		
		
		'    SqlStr = "Select SECTIONMST.NAME, SECTIONMST.SECTIONCODE," & vbCrLf _
		''        & " TRN.COMPANY_CODE, CHALLANNO , CHALLANDATE," & vbCrLf _
		''        & " TDS_AMOUNT AS TDSAMOUNT," & vbCrLf _
		''        & " SURCHARGE, EDU_CESS, INTEREST_AMOUNT, OTHER_AMOUNT," & vbCrLf _
		''        & " AMOUNT AS NET_AMOUNT," & vbCrLf _
		''        & " BANKCODE, CHQ_NO, CHQ_DATE,MKEY" & vbCrLf _
		''
		'    SqlStr = SqlStr & vbCrLf _
		''        & " FROM vwTDS_CHALLAN TRN, vwTDS_SECTION_MST SECTIONMST " & vbCrLf _
		''        & " WHERE "
		'
		'    SqlStr = SqlStr & vbCrLf _
		''        & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf _
		''        & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf _
		''        & " AND ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf _
		''        & " AND TRN.FROMDATE>='" & Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf _
		''        & " AND TRN.TODATE<='" & Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		'
		'    If chkConsolidated.Value = vbUnchecked Then
		'       SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany!COMPANY_CODE & ""
		'    End If
		'
		'    SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany!FYEAR & ""
		'
		'    SqlStr = SqlStr & vbCrLf _
		''        & " ORDER BY TRN.COMPANY_CODE, MKEY, SECTIONMST.NAME, MKEY, CHALLANDATE, CHALLANNO "
		
		SqlStr = GetChallanQry(False)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdView26
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.Row = cntRow
					.Col = 1
					.Text = CStr(cntRow)
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("SECTIONCODE").Value), "", RsTemp.Fields("SECTIONCODE").Value) ''IIf(IsNull(RsTemp!Name), "", RsTemp!Name)
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDSAMOUNT").Value), "", RsTemp.Fields("TDSAMOUNT").Value), "0.00")
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURCHARGE").Value), "", RsTemp.Fields("SURCHARGE").Value), "0.00")
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EDU_CESS").Value), "", RsTemp.Fields("EDU_CESS").Value), "0.00")
					
					.Col = 6
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INTEREST_AMOUNT").Value), "", RsTemp.Fields("INTEREST_AMOUNT").Value), "0.00")
					
					.Col = 7
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OTHER_AMOUNT").Value), "", RsTemp.Fields("OTHER_AMOUNT").Value), "0.00")
					
					.Col = 8
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("NET_AMOUNT").Value), "", RsTemp.Fields("NET_AMOUNT").Value), "0.00")
					
					.Col = 9
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHQ_NO").Value), "", RsTemp.Fields("CHQ_NO").Value)
					
					.Col = 10
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("BANKCODE").Value), "", RsTemp.Fields("BANKCODE").Value)
					
					.Col = 11
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")
					
					.Col = 12
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHALLANNO").Value), "", RsTemp.Fields("CHALLANNO").Value)
					
					.Col = 13
					.Text = ""
					
					.Col = 14
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
					.Col = 15
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
					
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						cntRow = cntRow + 1
						.MaxRows = cntRow
					End If
				Loop 
			End If
		End With
		ShowDetail26 = True
		Exit Function
ErrPart1: 
		ShowDetail26 = False
	End Function
	
	Private Function GetChallanQry(ByRef IsInquery As Boolean) As String
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mTDSAccountCode As String
		Dim mOriginalRRRNo As String
		Dim mSqlStr As String
		
		
		'UPGRADE_WARNING: Untranslated statement in GetChallanQry. Please check source code.
		
		If IsInquery = False Then
			SqlStr = " Select SECTIONMST.NAME, SECTIONMST.SECTIONCODE," & vbCrLf & " TRN.COMPANY_CODE, CHALLANNO , CHALLANDATE," & vbCrLf & " TDS_AMOUNT AS TDSAMOUNT," & vbCrLf & " SURCHARGE, EDU_CESS, INTEREST_AMOUNT, OTHER_AMOUNT," & vbCrLf & " AMOUNT AS NET_AMOUNT," & vbCrLf & " BANKCODE, CHQ_NO, CHQ_DATE,MKEY"
		Else
			SqlStr = "Select MKEY"
		End If
		
		
		SqlStr = SqlStr & vbCrLf & " FROM vwTDS_CHALLAN TRN, vwTDS_SECTION_MST SECTIONMST " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf & " AND TRN.FROMDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.TODATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		If IsInquery = False Then
			SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.COMPANY_CODE, MKEY, SECTIONMST.NAME, MKEY, CHALLANDATE, CHALLANNO "
		End If
		
		GetChallanQry = SqlStr
		Exit Function
ErrPart1: 
		GetChallanQry = ""
	End Function
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
		Call FormatSprdView26()
		Call FormatSprdViewAnnex()
	End Sub
	
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView26()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdView26
			.MaxCols = 15
			
			.set_RowHeight(0, RowHeight * 3)
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 8)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 6)
			
			For i = 3 To 8
				.Col = i
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMin = CDbl("0")
				.TypeFloatMax = CDbl("9999999999")
				.TypeFloatMoney = False
				.TypeFloatSeparator = False
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatSepChar = Asc(",")
				.set_ColWidth(i, 10)
			Next 
			
			.Col = 9
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 7)
			
			.Col = 10
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 7)
			
			.Col = 11
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
			.set_ColWidth(.Col, 12)
			
			.Col = 13
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			.Col = 14
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			.Col = 15
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			
			FillHeadingSprdView26()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView26, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView26, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex
			.MaxCols = 25
			
			.set_RowHeight(0, RowHeight * 3.5)
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 8)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 6)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 12)
			
			.Col = 4
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 25)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			For i = 6 To 6
				.Col = i
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMin = CDbl("0")
				.TypeFloatMax = CDbl("9999999999")
				.TypeFloatMoney = False
				.TypeFloatSeparator = False
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatSepChar = Asc(",")
				.set_ColWidth(i, 10)
			Next 
			
			.Col = 7
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			
			For i = 8 To 12
				.Col = i
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMin = CDbl("0")
				.TypeFloatMax = CDbl("9999999999")
				.TypeFloatMoney = False
				.TypeFloatSeparator = False
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatSepChar = Asc(",")
				.set_ColWidth(i, 10)
			Next 
			
			.Col = 13
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			For i = 14 To 14
				.Col = i
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalPlaces = 4
				.TypeFloatMin = CDbl("0")
				.TypeFloatMax = CDbl("9999999999")
				.TypeFloatMoney = False
				.TypeFloatSeparator = False
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatSepChar = Asc(",")
				.set_ColWidth(i, 10)
			Next 
			
			For i = 15 To 21
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 10)
			Next 
			
			For i = 22 To 25
				.Col = i
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalPlaces = 2
				.TypeFloatMin = CDbl("0")
				.TypeFloatMax = CDbl("9999999999")
				.TypeFloatMoney = False
				.TypeFloatSeparator = False
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatSepChar = Asc(",")
				.set_ColWidth(i, 10)
			Next 
			
			FillHeadingSprdViewAnnex()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewAnnex, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewAnnex, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	
	
	
	Private Sub FillHeadingSprdViewAnnex()
		
		With SprdViewAnnex
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(414)" & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Deductee Code (01-Company 02-Other Than company)" & vbNewLine & "(415)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "PAN of the Deductee" & vbNewLine & "(416)" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Name of the Deductee" & vbNewLine & "(417)" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Date of payment / Credit" & vbNewLine & "(418)" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Amount paid / Credited Rs." & vbNewLine & "(419)" & vbNewLine & "(6)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Paid by book entry or otherwise" & vbNewLine & "(420)" & vbNewLine & "(7)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "TDS" & vbNewLine & "(421)" & vbNewLine & "(8)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Surcharge" & vbNewLine & "(422)" & vbNewLine & "(9)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Education Cess" & vbNewLine & "(423)" & vbNewLine & "(10)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Total tax deducted(421+422+423) Rs." & vbNewLine & "(424)" & vbNewLine & "(11)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Total Tax deposited" & vbNewLine & "(425)" & vbNewLine & "(12)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Date of deduction" & vbNewLine & "(426)" & vbNewLine & "(13)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Rate at which dedicted" & vbNewLine & "(427)" & vbNewLine & "(14)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Reason for non-deduction/lower deduction" & vbNewLine & "(428)" & vbNewLine & "(15)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "Challan Mkey"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "BSR CODE"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 19
			.Text = "Deposited Date"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 20
			.Text = "Challan Serial No"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 21
			.Text = "Section Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 22
			.Text = "Total TDS"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 23
			.Text = "Interest"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 24
			.Text = "Others"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 25
			.Text = "Total of the Above"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	
	
	Private Sub FillHeadingSprdView26()
		
		With SprdView26
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(401)" & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Section Code" & vbNewLine & "(402)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "TDS Rs." & vbNewLine & "(403)" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Surcharge Rs." & vbNewLine & "(404)" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Education Cess Rs." & vbNewLine & "(405)" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Interest Rs." & vbNewLine & "(406)" & vbNewLine & "(6)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Others Rs." & vbNewLine & "(407)" & vbNewLine & "(7)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Total Tax deposited Rs." & vbNewLine & "(408)" & vbNewLine & "(8)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Cheque/DD No." & vbNewLine & "(409)" & vbNewLine & "(9)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "BSR Code" & vbNewLine & "(410)" & vbNewLine & "(10)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Date on which tax deposted" & vbNewLine & "(411)" & vbNewLine & "(11)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Transfer Voucher/Challan Serial Number" & vbNewLine & "(412)" & vbNewLine & "(12)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Whether TDS deposited by book entry (Y/N/)" & vbNewLine & "(413)" & vbNewLine & "(13)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "MKEY"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub frmTDSeReturn26QRevise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		FormActive = False
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		Dim mMonthType As String
		Dim mProvReceiptNo As String
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTDSAcNo.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPanNo.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		txtTDSAcNo.Enabled = False
		txtPanNo.Enabled = False
		
		txtFYear.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
		txtAYear.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")
		
		
		If chkRefilling.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			txtReturnFiled.Text = "NO"
			txtProvReceiptNo.Text = ""
		Else
			txtReturnFiled.Text = "YES"
			'UPGRADE_WARNING: Couldn't resolve default property of object GetProvReceiptNo(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			mProvReceiptNo = GetProvReceiptNo()
			txtProvReceiptNo.Text = mProvReceiptNo
		End If
		
		txtProvReceiptNo.Text = ""
		
		txtPersonName.Text = RsCompany.Fields("COMPANY_NAME").Value
		txtDeductorType.Text = "Others"
		txtBranch.Text = ""
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtFlat.Text = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtBuilding.Text = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
		txtRoad.Text = ""
		txtArea.Text = ""
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTown.Text = IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtState.Text = IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPinCode.Text = IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPhone.Text = IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtEmail.Text = IIf(IsDbNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPersonName_p.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtDesg.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtFlat_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtBuilding_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
		txtRoad_p.Text = ""
		txtArea_p.Text = ""
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTown_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtState_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPinCode_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPhone_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_PHONE").Value), "", RsCompany.Fields("REGD_PHONE").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtEmail_p.Text = IIf(IsDbNull(RsCompany.Fields("REGD_MAILID").Value), "", RsCompany.Fields("REGD_MAILID").Value)
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView26, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewAnnex, RowHeight)
		
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetProvReceiptNo() As Object
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim mFieldName As String
		
		
		SqlStr = "SELECT IV_QTR_NO from PAY_RTN_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			'UPGRADE_WARNING: Couldn't resolve default property of object GetProvReceiptNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			GetProvReceiptNo = IIf(IsDbNull(RsTemp.Fields("IV_QTR_NO").Value), "", RsTemp.Fields("IV_QTR_NO").Value)
		End If
		Exit Function
ErrPart: 
		'UPGRADE_WARNING: Couldn't resolve default property of object GetProvReceiptNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		GetProvReceiptNo = ""
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetLastChallanRecd() As Integer
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		
		GetLastChallanRecd = 0
		SqlStr = " Select COUNT(1) CNTRECD"
		
		SqlStr = SqlStr & vbCrLf & " FROM vwPAY_ITCHALLAN_HDR IH" & vbCrLf & " WHERE "
		
		'SqlStr = SqlStr & vbCrLf _
		'& " ID.COMPANY_CODE=EMP.COMPANY_CODE " ''& vbCrLf _
		'& " AND ID.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf _
		'& " AND ID.EMP_CODE=EMP.EMP_CODE"
		
		SqlStr = SqlStr & vbCrLf & " IH.BOOKTYPE<>'C'"
		
		SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND IH.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			GetLastChallanRecd = IIf(IsDbNull(RsTemp.Fields("CNTRECD").Value), 0, RsTemp.Fields("CNTRECD").Value)
		End If
		
		Exit Function
ErrPart1: 
		GetLastChallanRecd = 0
		'    Resume
	End Function
	Private Sub SetTextLength()
		On Error GoTo ERR1
		
		'UPGRADE_WARNING: TextBox property txtPersonName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPersonName.Maxlength = 75
		'UPGRADE_WARNING: TextBox property txtFlat.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtFlat.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtBuilding.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBuilding.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtRoad.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtRoad.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtArea.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtArea.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtTown.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtTown.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtState.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtState.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtPinCode.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPinCode.Maxlength = 6
		
		
		
		'UPGRADE_WARNING: TextBox property txtPersonName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPersonName.Maxlength = 75
		'UPGRADE_WARNING: TextBox property txtDeductorType.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDeductorType.Maxlength = 3
		'UPGRADE_WARNING: TextBox property txtBranch.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBranch.Maxlength = 40
		'UPGRADE_WARNING: TextBox property txtFlat.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtFlat.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtBuilding.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBuilding.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtRoad.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtRoad.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtArea.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtArea.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtTown.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtTown.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtState.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtState.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtPinCode.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPinCode.Maxlength = 6
		'UPGRADE_WARNING: TextBox property txtPhone.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPhone.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtEmail.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtEmail.Maxlength = 25
		
		
		'UPGRADE_WARNING: TextBox property txtPersonName_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPersonName_p.Maxlength = 75
		'UPGRADE_WARNING: TextBox property txtDesg.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDesg.Maxlength = 20
		'UPGRADE_WARNING: TextBox property txtFlat_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtFlat_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtBuilding_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtBuilding_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtRoad_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtRoad_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtArea_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtArea_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtTown_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtTown_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtState_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtState_p.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtPinCode_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPinCode_p.Maxlength = 6
		'UPGRADE_WARNING: TextBox property txtPhone_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPhone_p.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtEmail_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtEmail_p.Maxlength = 25
		
		Exit Sub
ERR1: 
		MsgBox(Err.Description)
	End Sub
	Private Function ShowDosReport(ByRef pPrintMode As String) As Boolean
		On Error GoTo ErrPart
		Dim pFileName As String
		Dim mLineCount As Integer
		Dim FilePath As String
		
		If lblFormType.Text = "26Q" Then
			pFileName = mPubTDSPath & "\eRtn26Q.txt"
		Else
			pFileName = mPubTDSPath & "\eRtn27Q.txt"
		End If
		
		FilePath = ""
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePath = Dir(mPubTDSPath, FileAttribute.Directory) '' Dir(pFileName)
		
		If FilePath = "" Then
			Call MkDir(mPubTDSPath)
		End If
		
		Call ShellAndContinue("ATTRIB +A -R " & pFileName)
		FileOpen(1, pFileName, OpenMode.Output)
		mLineCount = 1
		
		Call PrintFH(mLineCount)
		Call PrintBH(mLineCount)
		Call PrintCD(mLineCount)
		'    If cboCorrectionType.ListIndex = 3 Then
		'        Call PrintDD(mLineCount)
		'    End If
		'
		FileClose(1)
		
		
		'    If pPrintMode = "P" Then
		'        Dim mFP As Boolean
		'        mFP = Shell(App.path & "\PrintReport.bat", vbNormalFocus)
		'        If mFP = False Then GoTo ErrPart
		'    Else
		Shell("ATTRIB +R -A " & pFileName)
		Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
		'    End If
		
		ShowDosReport = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		ShowDosReport = False
		''Resume
		FileClose(1)
	End Function
	
	Private Function PrintFH(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		
		
		
		'''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		'''2
		mString = "FH"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''3
		mString = "NS1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''4
		mString = "C"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''5
		mString = VB6.Format(PubCurrDate, "DDMMYYYY")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''6
		mString = CStr(mLineCount)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''7
		mString = "D"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''8
		mString = Trim(txtTDSAcNo.Text)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''9
		mString = CStr(mLineCount)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''10 ''NEW-14102009
		If RsCompany.Fields("FYEAR").Value <= 2009 Then
			mString = "HEILERP"
		Else
			mString = "HEILERP" '' "HEILERP"
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''11
		mMainString = mMainString & mDelimited
		
		'''12
		mMainString = mMainString & mDelimited
		
		'''13
		mMainString = mMainString & mDelimited
		
		'''14
		mMainString = mMainString & mDelimited
		
		'''15
		mMainString = mMainString & mDelimited
		
		'''16
		mMainString = mMainString & mDelimited
		
		'''17
		'    If RsCompany!fyear <= 2009 Then
		'        mMainString = mMainString & mDelimited
		'    End If
		
		PrintLine(1, TAB(0), mMainString)
		
		mLineCount = mLineCount + 1
		
		PrintFH = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintFH = False
		'    Resume
	End Function
	
	Private Function PrintCD(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim cntCol As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim mTotDeductee As Double
		Dim mCompany_Code As Integer
		Dim mMkey As String
		Dim mDepositAmt As Double
		Dim mTDSAmount As Double
		Dim mSurchargeAmt As Double
		Dim mCESSAmt As Double
		Dim mNetAmount As Double
		Dim mIntAmt As Double
		Dim mOthAmt As Double
		Dim mCMkeyLineNo As Integer
		
		If cboCorrectionType.SelectedIndex = 0 Or cboCorrectionType.SelectedIndex = 5 Then
			PrintCD = True
			Exit Function
		End If
		
		With SprdView26
			For cntRow = 1 To .MaxRows
				
				.Row = cntRow
				.Col = 14
				mCompany_Code = Val(.Text)
				
				.Col = 15
				mMkey = .Text
				
				
				If GetChallan_DedDetail(mDepositAmt, mTDSAmount, mSurchargeAmt, mCESSAmt, mNetAmount, mIntAmt, mOthAmt, mTotDeductee, mCompany_Code, mMkey) = False Then GoTo ErrPart
				
				.Row = cntRow
				
				'''1
				mString = CStr(mLineCount)
				mMainString = mString
				mMainString = mMainString & mDelimited
				
				'''2
				mString = "CD"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''3
				mString = "1"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''4
				.Col = 1
				mString = CStr(Val(.Text))
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''5
				If cboCorrectionType.SelectedIndex = 2 Or cboCorrectionType.SelectedIndex = 3 Or cboCorrectionType.SelectedIndex = 4 Then
					mString = VB6.Format(mTotDeductee, "0")
				Else
					mString = ""
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''6
				mString = "N"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				
				'''7
				mMainString = mMainString & mDelimited
				
				'''8
				mMainString = mMainString & mDelimited
				
				'''9
				mMainString = mMainString & mDelimited
				
				'''10
				mMainString = mMainString & mDelimited
				
				'''11
				'            mMainString = mMainString & mDelimited
				.Col = 12
				mString = Trim(.Text)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''12
				'            .Col = 12
				'            mString = Trim(.Text)
				'            mMainString = mMainString & mString
				'            mMainString = mMainString & mDelimited
				mMainString = mMainString & mDelimited
				
				'''13
				mMainString = mMainString & mDelimited
				
				'''14
				mMainString = mMainString & mDelimited
				
				'''15
				.Col = 10
				mString = Trim(.Text)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''16
				mMainString = mMainString & mDelimited
				
				'''17
				.Col = 11
				mString = VB6.Format(.Text, "DDMMYYYY")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''18
				mMainString = mMainString & mDelimited
				
				
				
				'''19
				mMainString = mMainString & mDelimited
				
				'''20
				mMainString = mMainString & mDelimited
				
				'''21
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					.Col = 2
					mString = Trim(.Text) '''Mid(Trim(.Text), 2)
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''22 to 27
				If cboCorrectionType.SelectedIndex = 3 Then
					mMainString = mMainString & mDelimited
					mMainString = mMainString & mDelimited
					mMainString = mMainString & mDelimited
					mMainString = mMainString & mDelimited
					mMainString = mMainString & mDelimited
					mMainString = mMainString & mDelimited
				Else
					For cntCol = 3 To 8
						.Col = cntCol
						mString = VB6.Format(System.Math.Round(Val(.Text), 0), "0.00")
						mMainString = mMainString & mString
						mMainString = mMainString & mDelimited
					Next 
				End If
				
				
				
				'''28
				mString = VB6.Format(mDepositAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''29
				mMainString = mMainString & mDelimited
				
				'''30
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mString = VB6.Format(mTDSAmount, "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				
				'''31
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mString = VB6.Format(mSurchargeAmt, "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				
				'''32
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mString = VB6.Format(mCESSAmt, "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''33
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mNetAmount = mTDSAmount + mSurchargeAmt + mCESSAmt
					mString = VB6.Format(mNetAmount, "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''34
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mString = VB6.Format(System.Math.Round(mIntAmt, 0), "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''35
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					mString = VB6.Format(System.Math.Round(mOthAmt, 0), "0.00")
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''36
				If cboCorrectionType.SelectedIndex = 3 Then
					mString = ""
				Else
					.Col = 9
					mString = CStr(Val(.Text))
				End If
				
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''37
				mString = "" ''N
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''38
				mMainString = mMainString & mDelimited
				
				'''39
				'            If RsCompany!fyear <= 2009 Then
				'                mMainString = mMainString & mDelimited
				'            End If
				
				PrintLine(1, TAB(0), mMainString)
				mCMkeyLineNo = cntRow
				mLineCount = mLineCount + 1
				
				''Deductee Details
				Call PrintDD(mLineCount, mCompany_Code, mMkey, mCMkeyLineNo)
			Next 
		End With
		PrintCD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintCD = False
		'    Resume
	End Function
	
	Private Function PrintBH(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim mChangeInDeductor As String
		
		Dim mTotChallanNo As Double
		Dim mTotDeductee As Double
		Dim mChallanAmount As Double
		Dim mDeducteeAmount As Double
		
		If GetChallanDetail(mTotChallanNo, mTotDeductee, mChallanAmount, mDeducteeAmount) = False Then GoTo ErrPart
		
		'''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		'''2
		mString = "BH"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''3
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''4
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = ""
		ElseIf cboCorrectionType.SelectedIndex = 1 Then 
			mString = CStr(mTotChallanNo)
		ElseIf cboCorrectionType.SelectedIndex = 2 Then 
			mString = CStr(mTotChallanNo)
		ElseIf cboCorrectionType.SelectedIndex = 3 Then 
			mString = CStr(mTotChallanNo)
		ElseIf cboCorrectionType.SelectedIndex = 4 Then 
			mString = CStr(mTotChallanNo)
		ElseIf cboCorrectionType.SelectedIndex = 5 Then 
			mString = ""
		End If
		
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		
		''' 5
		mString = UCase(lblFormType.Text) ''"26Q"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 6
		
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = "C1"
		ElseIf cboCorrectionType.SelectedIndex = 1 Then 
			mString = "C2"
		ElseIf cboCorrectionType.SelectedIndex = 2 Then 
			mString = "C3"
		ElseIf cboCorrectionType.SelectedIndex = 3 Then 
			mString = "C5"
		ElseIf cboCorrectionType.SelectedIndex = 4 Then 
			mString = "C9"
		ElseIf cboCorrectionType.SelectedIndex = 5 Then 
			mString = "Y"
		End If
		
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 7
		mChangeInDeductor = "0" 'if change in deductor then 1 else 0
		
		If cboCorrectionType.SelectedIndex = 1 Then
			mString = mChangeInDeductor
		ElseIf cboCorrectionType.SelectedIndex = 2 Then 
			mString = mChangeInDeductor
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 8
		mString = Trim(txtProvReceiptNo.Text)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 9
		mString = Trim(txtProvReceiptNo.Text)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 10
		mMainString = mMainString & mDelimited
		
		''' 11
		mMainString = mMainString & mDelimited
		
		''' 12
		mString = Trim(txtTDSAcNo.Text)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''13
		If cboCorrectionType.SelectedIndex = 3 Then
			mString = ""
		Else
			mString = Trim(txtTDSAcNo.Text)
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''14
		mMainString = mMainString & mDelimited
		
		'''15
		If cboCorrectionType.SelectedIndex = 3 Or cboCorrectionType.SelectedIndex = 2 Then
			mString = ""
		Else
			mString = Trim(txtPanNo.Text)
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''16
		mString = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''17
		mString = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''18
		If Month(CDate(txtDateTo.Text)) = 4 Or Month(CDate(txtDateTo.Text)) = 5 Or Month(CDate(txtDateTo.Text)) = 6 Then
			mString = "Q1"
		ElseIf Month(CDate(txtDateTo.Text)) = 7 Or Month(CDate(txtDateTo.Text)) = 8 Or Month(CDate(txtDateTo.Text)) = 9 Then 
			mString = "Q2"
		ElseIf Month(CDate(txtDateTo.Text)) = 10 Or Month(CDate(txtDateTo.Text)) = 11 Or Month(CDate(txtDateTo.Text)) = 12 Then 
			mString = "Q3"
		ElseIf Month(CDate(txtDateTo.Text)) = 1 Or Month(CDate(txtDateTo.Text)) = 2 Or Month(CDate(txtDateTo.Text)) = 3 Then 
			mString = "Q4"
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''19
		mString = VB.Left(Trim(txtPersonName.Text), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''20
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtBranch.Text), 75)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''21
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtFlat.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''22
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtBuilding.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''23
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtRoad.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''24
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtArea.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''25
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtTown.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''26
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = GetStateCode_TDS((txtState.Text))
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''27
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB6.Format(Val(txtPinCode.Text), "000000")
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''28
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtEmail.Text), 75)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''29
		If cboCorrectionType.SelectedIndex = 0 Then
			If Trim(txtPhone.Text) = "" Then
				mString = ""
			Else
				mString = Trim(VB.Left(txtPhone.Text, 4))
			End If
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''30
		If cboCorrectionType.SelectedIndex = 0 Then
			If Trim(txtPhone.Text) = "" Then
				mString = ""
			Else
				mString = Mid(txtPhone.Text, 6, 7)
			End If
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''31
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = "N"
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''32
		mString = "K"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''33
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtPersonName_p.Text), 75)
		Else
			mString = ""
		End If
		
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''34
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtDesg.Text), 20)
		Else
			mString = ""
		End If
		
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''35
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtFlat_p.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''36
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtBuilding_p.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''37
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtRoad_p.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''38
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtArea_p.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''39
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtTown_p.Text), 25)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''40
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = GetStateCode_TDS((txtState_p.Text))
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''41
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB6.Format(Val(txtPinCode_p.Text), "000000")
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''42
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = VB.Left(Trim(txtEmail_p.Text), 75)
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''43
		mString = ""
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''44
		If cboCorrectionType.SelectedIndex = 0 Then
			If Trim(txtPhone_p.Text) = "" Then
				mString = ""
			Else
				mString = Trim(VB.Left(txtPhone_p.Text, 4))
			End If
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''45
		If cboCorrectionType.SelectedIndex = 0 Then
			If Trim(txtPhone_p.Text) = "" Then
				mString = ""
			Else
				mString = Mid(txtPhone_p.Text, 6, 7)
			End If
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''46
		If cboCorrectionType.SelectedIndex = 0 Then
			mString = IIf(chkPersonAddChange.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''47
		If cboCorrectionType.SelectedIndex = 1 Or cboCorrectionType.SelectedIndex = 2 Or cboCorrectionType.SelectedIndex = 4 Then
			mString = VB6.Format(mChallanAmount, "0.00")
		Else
			mString = ""
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''48
		mMainString = mMainString & mDelimited
		
		'''49
		mMainString = mMainString & mDelimited
		
		'''50
		mMainString = mMainString & mDelimited
		
		'''51
		mString = "N"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''52
		mMainString = mMainString & mDelimited
		
		'''53
		mMainString = mMainString & mDelimited
		
		'    If RsCompany!FYEAR <= 2009 Then
		'''54
		mMainString = mMainString & mDelimited
		
		'''55
		mMainString = mMainString & mDelimited
		
		'''56
		mMainString = mMainString & mDelimited
		
		'''57
		mMainString = mMainString & mDelimited
		
		'''58
		mMainString = mMainString & mDelimited
		
		'''59
		mMainString = mMainString & mDelimited
		
		'''60
		mMainString = mMainString & mDelimited
		
		'''61
		mMainString = mMainString & mDelimited
		
		'''62
		'        mMainString = mMainString & mDelimited
		'    End If
		
		PrintLine(1, TAB(0), mMainString)
		
		mLineCount = mLineCount + 1
		
		PrintBH = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintBH = False
		'    Resume
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetChallanDetail(ByRef pTotChallanNo As Double, ByRef pTotDeductee As Double, ByRef pChallanAmount As Double, ByRef pDeducteeAmount As Double) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mTDSAccountCode As String
		
		pTotChallanNo = 0
		pTotDeductee = 0
		pChallanAmount = 0
		pDeducteeAmount = 0
		
		'UPGRADE_WARNING: Untranslated statement in GetChallanDetail. Please check source code.
		
		SqlStr = "Select COUNT(CHALLANNO) TOTCHALLANNO, SUM(ROUND(AMOUNT,0)) AS TDSAMOUNT "
		
		SqlStr = SqlStr & vbCrLf & " FROM vwTDS_CHALLAN TRN, vwTDS_SECTION_MST SECTIONMST " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf & " AND TRN.FROMDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.TODATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pTotChallanNo = IIf(IsDbNull(RsTemp.Fields("TOTCHALLANNO").Value), 0, RsTemp.Fields("TOTCHALLANNO").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pChallanAmount = IIf(IsDbNull(RsTemp.Fields("TDSAMOUNT").Value), 0, RsTemp.Fields("TDSAMOUNT").Value)
			pChallanAmount = System.Math.Round(pChallanAmount, 0)
		End If
		
		SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(TDSAMOUNT) TOTTDSAMOUNT "
		
		SqlStr = SqlStr & vbCrLf & " FROM vwTDS_TRN, vwTDS_SECTION_MST SECTIONMST,vwFIN_SUPP_CUST_MST CMST "
		
		SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE(+)" & vbCrLf & " AND TRN.PARTYNAME=CMST.SUPP_CUST_NAME(+)" & vbCrLf & " AND TRN.CANCELLED='N' AND TRN.ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf & " AND TRN.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.CHALLANMKEY IN  ( " & vbCrLf & " " & GetChallanQry(True) & ")"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pTotDeductee = IIf(IsDbNull(RsTemp.Fields("TOTDEDUCTEE").Value), 0, RsTemp.Fields("TOTDEDUCTEE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pDeducteeAmount = IIf(IsDbNull(RsTemp.Fields("TOTTDSAMOUNT").Value), 0, RsTemp.Fields("TOTTDSAMOUNT").Value)
		End If
		
		
		GetChallanDetail = True
		Exit Function
ErrPart1: 
		GetChallanDetail = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetChallan_DedDetail(ByRef pDepositAmt As Double, ByRef pTDSAmount As Double, ByRef pSurchargeAmt As Double, ByRef pCessAmt As Double, ByRef pNetAmount As Double, ByRef pIntAmt As Double, ByRef pOthAmt As Double, ByRef pTotDeductee As Double, ByRef pCompany_Code As Integer, ByRef pMkey As String) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mTDSAccountCode As String
		
		pTotDeductee = 0
		
		pTotDeductee = 0
		pDepositAmt = 0
		pTDSAmount = 0
		pSurchargeAmt = 0
		pCessAmt = 0
		pNetAmount = 0
		pIntAmt = 0
		pOthAmt = 0
		
		'UPGRADE_WARNING: Untranslated statement in GetChallan_DedDetail. Please check source code.
		
		SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(TRN.AMOUNTPAID) AS DEPOSIT_AMOUNT, " & vbCrLf & " SUM(TDSAMOUNT) AS TOTTDSAMOUNT, " & vbCrLf & " SUM(SURCHARGE) AS TOTSURCHARGE, " & vbCrLf & " SUM(EDU_CESS) AS TOTEDU_CESS, " & vbCrLf & " SUM(NET_AMOUNT) AS TOTNET_AMOUNT, " & vbCrLf & " SUM(INTEREST_AMOUNT) AS TOTINTEREST_AMOUNT, " & vbCrLf & " SUM(OTHER_AMOUNT) AS TOTOTHER_AMOUNT "
		
		SqlStr = SqlStr & vbCrLf & " FROM vwTDS_TRN, vwTDS_SECTION_MST SECTIONMST, vwFIN_SUPP_CUST_MST CMST "
		
		SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=SECTIONMST.COMPANY_CODE" & vbCrLf & " AND TRN.SECTIONCODE=SECTIONMST.CODE" & vbCrLf & " AND TRN.COMPANY_CODE=CMST.COMPANY_CODE(+)" & vbCrLf & " AND TRN.PARTYNAME=CMST.SUPP_CUST_NAME(+)" & vbCrLf & " AND TRN.CHALLANMKEY='" & pMkey & "' " & vbCrLf & " AND TRN.CANCELLED='N' AND TRN.ACCOUNTCODE='" & mTDSAccountCode & "'" & vbCrLf & " AND TRN.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=" & pCompany_Code & ""
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pTotDeductee = IIf(IsDbNull(RsTemp.Fields("TOTDEDUCTEE").Value), 0, RsTemp.Fields("TOTDEDUCTEE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pTDSAmount = IIf(IsDbNull(RsTemp.Fields("TOTTDSAMOUNT").Value), 0, RsTemp.Fields("TOTTDSAMOUNT").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pSurchargeAmt = IIf(IsDbNull(RsTemp.Fields("TOTSURCHARGE").Value), 0, RsTemp.Fields("TOTSURCHARGE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pCessAmt = IIf(IsDbNull(RsTemp.Fields("TOTEDU_CESS").Value), 0, RsTemp.Fields("TOTEDU_CESS").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pNetAmount = IIf(IsDbNull(RsTemp.Fields("TOTNET_AMOUNT").Value), 0, RsTemp.Fields("TOTNET_AMOUNT").Value)
			
			pDepositAmt = pTDSAmount + pSurchargeAmt + pCessAmt
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pIntAmt = IIf(IsDbNull(RsTemp.Fields("TOTINTEREST_AMOUNT").Value), 0, RsTemp.Fields("TOTINTEREST_AMOUNT").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pOthAmt = IIf(IsDbNull(RsTemp.Fields("TOTOTHER_AMOUNT").Value), 0, RsTemp.Fields("TOTOTHER_AMOUNT").Value)
		End If
		
		
		GetChallan_DedDetail = True
		Exit Function
ErrPart1: 
		GetChallan_DedDetail = False
	End Function
	Private Function PrintDD(ByRef mLineCount As Integer, ByRef pCompany_Code As Integer, ByRef pMkey As String, ByRef pChallanLineNo As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim cntCol As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim mDeducteeRec As Integer
		
		If cboCorrectionType.SelectedIndex = 0 Or cboCorrectionType.SelectedIndex = 1 Or cboCorrectionType.SelectedIndex = 5 Then
			PrintDD = True
			Exit Function
		End If
		
		With SprdViewAnnex
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 17
				If pMkey = Trim(.Text) Then
					'                If mLineCount = 1073 Then MsgBox "OK"
					'''1
					mString = CStr(mLineCount)
					mMainString = mString
					mMainString = mMainString & mDelimited
					
					'''2
					mString = "DD"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''3
					mString = "1"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''4
					mString = CStr(pChallanLineNo)
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''5
					.Col = 1
					mString = CStr(Val(.Text))
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''6
					mString = "" ''"O"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''7
					mMainString = mMainString & mDelimited
					
					'''8
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 2
						mString = CStr(Val(.Text))
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''9
					mMainString = mMainString & mDelimited
					
					
					'''10
					.Col = 3
					If Len(Trim(.Text)) = 10 Then
						mString = UCase(Trim(.Text))
					Else
						mString = "PANINVALID"
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''11
					mMainString = mMainString & mDelimited
					
					'''12
					mMainString = mMainString & mDelimited
					
					'''13
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 4
						mString = VB.Left(UCase(Trim(.Text)), 75)
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					
					'''14 to 17
					If cboCorrectionType.SelectedIndex = 3 Then
						mMainString = mMainString & mDelimited
						mMainString = mMainString & mDelimited
						mMainString = mMainString & mDelimited
						mMainString = mMainString & mDelimited
					Else
						For cntCol = 8 To 11
							.Col = cntCol
							mString = VB6.Format(.Text, "0.00")
							mMainString = mMainString & mString
							mMainString = mMainString & mDelimited
						Next 
					End If
					
					'''18
					If cboCorrectionType.SelectedIndex = 3 Then
						.Col = 11
						mString = VB6.Format(.Text, "0.00")
					Else
						mString = ""
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''19
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 12
						mString = VB6.Format(.Text, "0.00")
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''20
					If cboCorrectionType.SelectedIndex = 3 Then
						.Col = 12
						mString = VB6.Format(.Text, "0.00")
					Else
						mString = ""
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''21
					mMainString = mMainString & mDelimited
					
					'''22
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 6
						mString = VB6.Format(.Text, "0.00")
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''23
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 5
						mString = VB6.Format(.Text, "DDMMYYYY")
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''24
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 13
						mString = VB6.Format(.Text, "DDMMYYYY")
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''25
					mMainString = mMainString & mDelimited
					
					'''26
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						.Col = 14
						mString = VB6.Format(.Text, "0.0000")
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''27
					mMainString = mMainString & mDelimited
					
					'''28
					If cboCorrectionType.SelectedIndex = 3 Then
						mString = ""
					Else
						mString = "N"
					End If
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''29
					mMainString = mMainString & mDelimited
					
					'''30
					mMainString = mMainString & mDelimited
					
					'''31
					mMainString = mMainString & mDelimited
					
					'''32
					mMainString = mMainString & mDelimited
					
					'''33
					'                mMainString = mMainString & mDelimited
					
					PrintLine(1, TAB(0), mMainString)
					
					mLineCount = mLineCount + 1
					
					
				End If
			Next 
		End With
		PrintDD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintDD = False
		'    Resume
	End Function
	
	
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