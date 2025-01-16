Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTDSeReturn24
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
		frmPrintTDS.OptForm26.Text = "Form 24"
		frmPrintTDS.OptFormChallan.Text = "Form 24 (Challan)"
		frmPrintTDS.OptFormChallan.Enabled = True
		frmPrintTDS.ShowDialog()
		
		If G_PrintLedg = False Then
			Exit Sub
		End If
		
		Call InsertIntoPrintDummy()
		
		If frmPrintTDS.OptForm26.Checked = True Then
			mTitle = "Form No. 24"
			mSubTitle = "[See section 192 and rule 37]"
			
			mReportFileName = "TDSeReturn24.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 1)
		ElseIf frmPrintTDS.OptFormChallan.Checked = True Then 
			mTitle = ""
			mSubTitle = ""
			
			mReportFileName = "TDSeReturnChallan.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 1)
		ElseIf frmPrintTDS.OptForm27A.Checked = True Then 
			
			mTitle = "Form No. 27A"
			mSubTitle = "[See rule 37B"
			
			mReportFileName = "TDSeReturn27A.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, 3)
		Else
			mTitle = "A N N E X U R E"
			mSubTitle = ""
			
			mReportFileName = "TDSeReturn24Annx.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
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
			If InsertGridDetail(SprdView24, 1, (SprdView24.MaxRows), (SprdView24.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptFormChallan.Checked = True Then 
			If InsertGridDetail(SprdViewChallan, 2, (SprdViewChallan.MaxRows), (SprdViewChallan.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			If InsertGridDetail(SprdViewAnnex, 4, (SprdViewAnnex.MaxRows), (SprdViewAnnex.MaxCols)) = False Then GoTo ERR1
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
		Dim mCol29 As String
		Dim mCol30 As String
		Dim mCol31 As String
		Dim mCol32 As String
		
		
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
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 29
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol29 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 30
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol30 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 31
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol31 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				.Col = 32
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Text. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				mCol32 = Trim(.Text)
				
				'UPGRADE_WARNING: Couldn't resolve default property of object mSprd.Col. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If mMaxCol = .Col Then GoTo InsertPart
				
InsertPart: 
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow,Field1,Field2,Field3, " & vbCrLf & " Field4,Field5,Field6,Field7,Field8,Field9, " & vbCrLf & " Field10,Field11,Field12,Field13,Field14,Field15,Field16," & vbCrLf & " Field17,Field18,Field19,Field20,Field21,Field22,Field23," & vbCrLf & " Field24,Field25,Field26,Field27,Field28," & vbCrLf & " Field29,Field30,Field31,Field32" & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol1) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol2) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol3) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol4) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol5) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol6) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol7) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol8) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol9) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol10) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol11) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol12) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol13) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol14) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol15) & "', "
				
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol16) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol17) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol18) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol19) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol20) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol21) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol22) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol23) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol24) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol25) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol26) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol27) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol28) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol29) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol30) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol31) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mCol32) & "' )"
				
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
		On Error GoTo ErrPart
		Dim mFormTitle As String
		Dim mPartyName As String
		Dim mFormName As String
		
		Dim mTotChallanNo As Double
		Dim mTotDeductee As Double
		Dim mChallanAmount As Double
		Dim mDeducteeAmount As Double
		Dim mTotPerquisiteRecd As Double
		
		Dim cntRow As Integer
		Dim mTANNo As String
		Dim mPANNo As String
		Dim mAYEAR As String
		Dim mFYear As String
		Dim mAmountPaid As Double
		
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		mFormName = "24"
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mTANNo = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mPANNo = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TANNo=""" & Trim(mTANNo) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PANNo=""" & Trim(mPANNo) & """")
		
		If frmPrintTDS.OptForm26.Checked = True Or frmPrintTDS.OptForm27A.Checked = True Then
			mFormTitle = "Annual return of 'Salaries' under section 206 of the Income-tax Act, 1961 for the year ending 31st March, " & VB6.Format(txtDateTo.Text, "YYYY")
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & Trim(txtDeductorType.Text) & """")
			
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
			'        MainClass.AssignCRptFormulas Report1, "PersonName_P=""" & txtPersonName_p.Text & """"
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
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			mFormTitle = "Particulars of values of perquities and amount of accretion to Employee's Provident Fund Account for the Year ending 31st March, " & VB6.Format(txtDateTo.Text, "YYYY")
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AuthName=""" & Trim(txtPersonName_p.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Designation=""" & Trim(txtDesg.Text) & """")
		
		If frmPrintTDS.OptForm27A.Checked = True Then
			
			If GetChallanDetail(mTotChallanNo, mTotDeductee, mTotPerquisiteRecd, mChallanAmount, mDeducteeAmount, mAmountPaid) = False Then GoTo ErrPart
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAmountPaid=""" & VB6.Format(mAmountPaid, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotDeduct=""" & VB6.Format(mDeducteeAmount, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotPerson=""" & mTotDeductee & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "FormName=""" & mFormName & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotChallanAmount=""" & VB6.Format(mChallanAmount, "0.00") & """")
			
			mFYear = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
			mAYEAR = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & mFYear & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & mAYEAR & """")
			'        MainClass.AssignCRptFormulas Report1, "ProReceiptNo=""" & Trim(txtProvReceiptNo.Text) & """"
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAnnexNo=""1""")
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "DeductorType=""" & UCase(Trim(txtDeductorType.Text)) & """")
			'        MainClass.AssignCRptFormulas Report1, "BRANCHNAME=""" & Trim(txtBranch.Text) & """"
			
		End If
		
		
		' Report1.CopiesToPrinter = PrintCopies
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		
		Report1.MarginLeft = 0
		Report1.MarginRight = 0
		
		Report1.Action = 1
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number))
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
		Dim MainClass_Renamed As Object
		Dim mSection As String
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		
		mSqlStr = mSqlStr & " ORDER BY SUBROW"
		
		FetchRecordForReport = mSqlStr
		
	End Function
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForTDS(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
		
		'    If Trim(TxtAccount) = "" Then
		'        MsgInformation "Please Enter Valid TDS Account Name."
		'        TxtAccount.SetFocus
		'        FieldsVerification = False
		'        Exit Function
		'    End If
		'
		'    If MainClass.ValidateWithMasterTable(TxtAccount, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany!COMPANY_CODE & " AND HEADTYPE='T'") = False Then
		'        MsgInformation "Please Enter Valid TDS Account Name."
		'        TxtAccount.SetFocus
		'        FieldsVerification = False
		'        Exit Function
		'    End If
		
		FieldsVerification = True
		Exit Function
ERR1: 
		FieldsVerification = False
	End Function
	
	Private Sub cmdValidate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdValidate.Click
		Call Shell(My.Application.Info.DirectoryPath & "\NeweReturn.exe", AppWinStyle.NormalFocus)
	End Sub
	
	'UPGRADE_WARNING: Form event frmTDSeReturn24.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Public Sub frmTDSeReturn24_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		On Error GoTo ERR1
		If FormActive = True Then Exit Sub
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		Call PrintStatus(False)
		FormatSprdView()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		FormActive = True
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTDSeReturn24_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Show1()
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim SqlStr As String
		Dim mSectionCode As Integer
		Dim cntRow As Integer
		Dim mEmpSnoIn26 As Integer
		Dim mCompanyCode As Integer
		Dim mEmpCode As String
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		If ShowDetail24 = False Then GoTo ErrPart
		If ShowDetailChallan = False Then GoTo ErrPart
		If ShowDetailAnnex = False Then GoTo ErrPart
		FormatSprdView()
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SortGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Call MainClass.SortGrid(SprdView24, 10, 3, True, False)
		
		With SprdView24
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				.Text = VB6.Format(cntRow, "0")
			Next 
		End With
		
		With SprdViewAnnex
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 17
				mCompanyCode = Val(.Text)
				
				.Col = 18
				mEmpCode = Trim(.Text)
				
				mEmpSnoIn26 = GetEMPSNoFROM24(mCompanyCode, mEmpCode)
				
				.Col = 2
				.Text = CStr(mEmpSnoIn26)
				
			Next 
		End With
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SortGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Call MainClass.SortGrid(SprdViewAnnex, 2, 1, False, False)
		
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
		Dim mAmount245 As Double
		Dim mAmount246 As Double
		Dim mAmount247 As Double
		Dim mAmount248 As Double
		Dim mAmount249 As Double
		Dim mAmount250 As Double
		Dim mAmount251 As Double
		Dim mAmount252 As Double
		Dim mAmount253 As Double
		Dim mAmount254 As Double
		Dim mAmount255 As Double
		Dim mAmount256 As Double
		Dim mAmount257 As Double
		Dim mAmount258 As Double
		Dim mAmount259 As Double
		Dim mAmount260 As Double
		Dim mEmpSnoIn26 As Integer
		
		
		SqlStr = " Select IH.*, " & vbCrLf & " EMP.EMP_PANNO,EMP.EMP_NAME "
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITFORM12BA_HDR IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_NAME,IH.COMPANY_CODE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdViewAnnex
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					mEmpSnoIn26 = GetEMPSNoFROM24(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("EMP_CODE").Value)
					
					If mEmpSnoIn26 = 0 Then GoTo NextRec
					
					.Row = cntRow
					.Col = 1
					.Text = CStr(cntRow)
					
					.Col = 1
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
					
					.Col = 2
					.Text = CStr(mEmpSnoIn26)
					
					.Col = 3
					mAmount247 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 1)
					.Text = VB6.Format(mAmount247, "0.00")
					
					.Col = 4
					mAmount248 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 1)
					.Text = VB6.Format(mAmount248, "0.00")
					
					.Col = 5
					mAmount249 = 0 ''GetAmountFromDetail12BA(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(mAmount249, "0.00")
					
					.Col = 6
					mAmount250 = mAmount249 * 0.01
					.Text = VB6.Format(mAmount250, "0.00")
					
					.Col = 7
					mAmount251 = mAmount248 + mAmount250
					.Text = VB6.Format(mAmount251, "0.00")
					
					.Col = 8
					mAmount252 = 0 '''GetAmountFromDetail12BA(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(mAmount252, "0.00")
					
					.Col = 9
					mAmount253 = mAmount247 - mAmount252
					.Text = VB6.Format(mAmount253, "0.00")
					
					.Col = 10
					mAmount254 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 2)
					.Text = VB6.Format(mAmount254, "0.00")
					
					.Col = 11
					mAmount255 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 3)
					.Text = VB6.Format(mAmount255, "0.00")
					
					.Col = 12
					mAmount256 = 0 ''GetAmountFromDetail12BA(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(mAmount256, "0.00")
					
					.Col = 13
					mAmount257 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 4)
					mAmount257 = mAmount257 + GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 5)
					.Text = VB6.Format(mAmount257, "0.00")
					
					.Col = 14
					mAmount258 = 0 '''GetAmountFromDetail12BA(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(mAmount258, "0.00")
					
					.Col = 15
					mAmount259 = 0 '''GetAmountFromDetail12BA(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(mAmount259, "0.00")
					
					.Col = 16
					mAmount260 = mAmount253 + mAmount254 + mAmount255 + mAmount256 + mAmount257 + mAmount258 + mAmount259
					.Text = VB6.Format(mAmount260, "0.00")
					
					
					.Col = 17
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
					.Col = 18
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value))
					
					Call CalcForm24(mEmpSnoIn26, mAmount260)
					
NextRec: 
					RsTemp.MoveNext()
					If RsTemp.EOF = False And mEmpSnoIn26 <> 0 Then
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
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetailChallan() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		
		cntRow = 1
		
		SqlStr = "Select " & vbCrLf & " AUTO_KEY_REFNO, VDATE, COMPANY_CODE, FYEAR, " & vbCrLf & " BOOKTYPE, AYEAR, CHALLANNO, CHALLANDATE, " & vbCrLf & " CHQ_NO, CHQ_DATE, BANKNAME, BSRCODE, " & vbCrLf & " TDS_AMOUNT, SURCHARGE, EDU_CESS, " & vbCrLf & " INTEREST_AMOUNT, OTHER_AMOUNT, NETAMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY CHALLANDATE,COMPANY_CODE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		cntRow = 1
		With SprdViewChallan
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.MaxRows = cntRow
					.Row = cntRow
					
					.Col = 1
					.Text = Str(cntRow)
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDS_AMOUNT").Value), 0, RsTemp.Fields("TDS_AMOUNT").Value), "0.00")
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURCHARGE").Value), 0, RsTemp.Fields("SURCHARGE").Value), "0.00")
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("EDU_CESS").Value), 0, RsTemp.Fields("EDU_CESS").Value), "0.00")
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INTEREST_AMOUNT").Value), 0, RsTemp.Fields("INTEREST_AMOUNT").Value), "0.00")
					
					.Col = 6
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("OTHER_AMOUNT").Value), 0, RsTemp.Fields("OTHER_AMOUNT").Value), "0.00")
					
					.Col = 7
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("NETAMOUNT").Value), 0, RsTemp.Fields("NETAMOUNT").Value), "0.00")
					
					.Col = 8
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHQ_NO").Value), "", RsTemp.Fields("CHQ_NO").Value)
					
					.Col = 9
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("BSRCODE").Value), "", RsTemp.Fields("BSRCODE").Value)
					
					.Col = 10
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")
					
					.Col = 11
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHALLANNO").Value), "", RsTemp.Fields("CHALLANNO").Value)
					
					.Col = 12
					.Text = "N"
					
					.Col = 13
					.Text = Str(RsCompany.Fields("COMPANY_CODE").Value)
					
					RsTemp.MoveNext()
					cntRow = cntRow + 1
				Loop 
			End If
		End With
		ShowDetailChallan = True
		Exit Function
ErrPart1: 
		'    Resume
		ShowDetailChallan = False
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetail24() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAmount205 As Double
		Dim mAmount206 As Double
		Dim mAmount207 As Double
		Dim mAmount208 As Double
		Dim mAmount209 As Double
		Dim mAmount210 As Double
		Dim mAmount211 As Double
		Dim mAmount212 As Double
		Dim mAmount213 As Double
		Dim mAmount214 As Double
		Dim mAmount215 As Double
		Dim mAmount216 As Double
		Dim mAmount217 As Double
		Dim mAmount218 As Double
		Dim mAmount219 As Double
		Dim mAmount220 As Double
		Dim mAmount221 As Double
		Dim mAmount222 As Double
		Dim mAmount223 As Double
		Dim mAmount224 As Double
		Dim mAmount225 As Double
		Dim mAmount226 As Double
		Dim mAmount227 As Double
		Dim mAmount228 As Double
		Dim mAmount229 As Double
		Dim mAmount230 As Double
		Dim mAmount231 As Double
		Dim mAmount232 As Double
		Dim mPANNo As String
		
		SqlStr = " Select IH.*, " & vbCrLf & " EMP.EMP_PANNO,EMP.EMP_NAME,EMP_SEX "
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITFORM16_HDR IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " AND (TAX_PAYABLE<>0 OR IH.COMPANY_CODE || IH.FYEAR || IH.EMP_CODE IN ( " & vbCrLf & " SELECT COMPANY_CODE || FYEAR || EMP_CODE " & vbCrLf & " FROM PAY_ITFORM16_DET " & vbCrLf & " WHERE SUBROW=16 " & vbCrLf & " AND TOTALAMOUNT>100000" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & "))"
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_NAME,IH.COMPANY_CODE "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdView24
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.Row = cntRow
					.Col = 1
					.Text = CStr(cntRow)
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mPANNo = IIf(IsDbNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
					.Text = IIf(Len(mPANNo) <> 10, "", mPANNo)
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("FROMDATE").Value), "", RsTemp.Fields("FROMDATE").Value), "DD/MM/YYYY")
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TODATE").Value), "", RsTemp.Fields("TODATE").Value), "DD/MM/YYYY")
					
					.Col = 7
					mAmount206 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 2, "TOTALAMOUNT")
					mAmount206 = mAmount206 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 3, "TOTALAMOUNT")
					mAmount206 = mAmount206 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 4, "TOTALAMOUNT")
					mAmount206 = mAmount206 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 24, "AMOUNT4")
					mAmount206 = mAmount206 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 25, "AMOUNT4")
					mAmount206 = mAmount206 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 26, "AMOUNT4")
					
					.Text = VB6.Format(mAmount206, "0.00")
					
					.Col = 8
					mAmount207 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 18)
					.Text = VB6.Format(mAmount207, "0.00")
					
					.Col = 9
					'                mAmount208 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 25, "AMOUNT4")
					'                mAmount208 = mAmount208 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 26, "AMOUNT4")
					mAmount208 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 28, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount208, "0.00")
					
					.Col = 6
					mAmount205 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 29, "TOTALAMOUNT")
					mAmount205 = mAmount205 - mAmount206 - mAmount207
					.Text = VB6.Format(mAmount205, "0.00")
					
					.Col = 10
					mAmount209 = mAmount205 + mAmount206 + mAmount207
					.Text = VB6.Format(mAmount209, "0.00")
					
					.Col = 11
					mAmount210 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 30, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount210, "0.00")
					
					.Col = 12
					mAmount211 = mAmount209 - mAmount210
					.Text = VB6.Format(mAmount211, "0.00")
					
					.Col = 13
					mAmount212 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 39, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount212, "0.00")
					
					.Col = 14
					mAmount213 = mAmount211 + mAmount212
					.Text = VB6.Format(mAmount213, "0.00")
					
					.Col = 15
					mAmount214 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 47, "AMOUNT1")
					.Text = VB6.Format(mAmount214, "0.00")
					
					.Col = 16
					mAmount215 = 0 '' GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 47, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount215, "0.00")
					
					.Col = 17
					mAmount216 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 47, "TOTALAMOUNT")
					mAmount216 = mAmount216 - mAmount214
					.Text = VB6.Format(mAmount216, "0.00")
					
					.Col = 18
					mAmount217 = mAmount214 + mAmount215 + mAmount216
					.Text = VB6.Format(mAmount217, "0.00")
					
					.Col = 19
					mAmount218 = mAmount213 - mAmount217
					.Text = VB6.Format(System.Math.Round(mAmount218, 0), "0.00")
					
					.Col = 20
					'                mAmount229 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 73, "TOTALAMOUNT")
					'                mAmount228 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 66, "TOTALAMOUNT")
					'                mAmount219 = mAmount229 + mAmount228 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 54, "TOTALAMOUNT")
					mAmount219 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 54, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount219, 0), "0.00")
					
					.Col = 21
					mAmount220 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 65, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount220, 0), "0.00")
					
					.Col = 22
					mAmount221 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 67, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount221, 0), "0.00")
					
					.Col = 23
					mAmount222 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 68, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount222, 0), "0.00")
					
					.Col = 24
					mAmount223 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 69, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount223, 0), "0.00")
					
					.Col = 25
					mAmount224 = mAmount219 - (mAmount220 + mAmount221 + mAmount222 + mAmount223)
					mAmount224 = mAmount224 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 73, "TOTALAMOUNT")
					mAmount224 = mAmount224 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 66, "TOTALAMOUNT")
					
					.Text = VB6.Format(System.Math.Round(mAmount224, 0), "0.00")
					
					.Col = 26
					mAmount225 = 0 ''GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(System.Math.Round(mAmount225, 0), "0.00")
					
					.Col = 27
					mAmount226 = mAmount224 - mAmount225
					.Text = VB6.Format(System.Math.Round(mAmount226, 0), "0.00")
					
					.Col = 29
					mAmount228 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 66, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount228, 0), "0.00")
					
					.Col = 30
					mAmount229 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 73, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount229, 0), "0.00")
					
					.Col = 31
					mAmount230 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 75, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount230, 0), "0.00")
					
					.Col = 28
					mAmount227 = mAmount230 - mAmount228 - mAmount229
					.Text = VB6.Format(System.Math.Round(mAmount227, 0), "0.00")
					
					.Col = 32
					mAmount231 = mAmount226 - mAmount230
					.Text = VB6.Format(System.Math.Round(mAmount231, 0), "0.00")
					
					.Col = 33
					.Text = ""
					
					.Col = 34
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
					
					.Col = 35
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						cntRow = cntRow + 1
						.MaxRows = cntRow
					End If
				Loop 
			End If
		End With
		ShowDetail24 = True
		Exit Function
ErrPart1: 
		'    Resume
		ShowDetail24 = False
	End Function
	
	Private Sub CalcForm24(ByRef pcntRow As Integer, ByRef pAmount260 As Double)
		On Error GoTo ErrPart1
		Dim mAmount205 As Double
		Dim mAmount206 As Double
		Dim mAmount207 As Double
		Dim mAmount208 As Double
		Dim mAmount209 As Double
		Dim mAmount210 As Double
		Dim mAmount211 As Double
		Dim mAmount212 As Double
		Dim mAmount213 As Double
		Dim mAmount214 As Double
		Dim mAmount215 As Double
		Dim mAmount216 As Double
		Dim mAmount217 As Double
		Dim mAmount218 As Double
		Dim mAmount219 As Double
		Dim mAmount220 As Double
		Dim mAmount221 As Double
		Dim mAmount222 As Double
		Dim mAmount223 As Double
		Dim mAmount224 As Double
		Dim mAmount225 As Double
		Dim mAmount226 As Double
		Dim mAmount227 As Double
		Dim mAmount228 As Double
		Dim mAmount229 As Double
		Dim mAmount230 As Double
		Dim mAmount231 As Double
		Dim mAmount232 As Double
		
		With SprdView24
			If .MaxRows < pcntRow Then Exit Sub
			.Row = pcntRow
			
			.Col = 6
			mAmount205 = Val(.Text)
			
			.Col = 7
			mAmount206 = Val(.Text)
			
			.Col = 8
			mAmount207 = CDbl(VB6.Format(pAmount260, "0.00"))
			.Text = VB6.Format(mAmount207, "0.00")
			
			.Col = 9
			mAmount208 = Val(.Text)
			
			.Col = 10
			mAmount209 = mAmount205 + mAmount206 + mAmount207
			.Text = VB6.Format(mAmount209, "0.00")
			
			.Col = 11
			mAmount210 = Val(.Text)
			
			.Col = 12
			mAmount211 = mAmount209 - mAmount210
			.Text = VB6.Format(mAmount211, "0.00")
			
			.Col = 13
			mAmount212 = Val(.Text)
			
			.Col = 14
			mAmount213 = mAmount211 + mAmount212
			.Text = VB6.Format(mAmount213, "0.00")
			
			.Col = 15
			mAmount214 = Val(.Text)
			.Text = VB6.Format(mAmount214, "0.00")
			
			.Col = 16
			mAmount215 = Val(.Text)
			.Text = VB6.Format(mAmount215, "0.00")
			
			.Col = 17
			mAmount216 = Val(.Text)
			.Text = VB6.Format(mAmount216, "0.00")
			
			.Col = 18
			mAmount217 = mAmount214 + mAmount215 + mAmount216
			.Text = VB6.Format(mAmount217, "0.00")
			
			.Col = 19
			mAmount218 = mAmount213 - mAmount217
			.Text = VB6.Format(mAmount218, "0.00")
			
			.Col = 20
			mAmount219 = Val(.Text)
			
			.Col = 21
			mAmount220 = Val(.Text)
			
			.Col = 22
			mAmount221 = Val(.Text)
			
			.Col = 23
			mAmount222 = Val(.Text)
			
			.Col = 24
			mAmount223 = Val(.Text)
			
			.Col = 25
			'        mAmount224 = mAmount219 - (mAmount220 + mAmount221 + mAmount222 + mAmount223)
			mAmount224 = Val(.Text)
			
			'        .Text = Format(mAmount224, "0.00")
			
			.Col = 26
			mAmount225 = Val(.Text)
			
			.Col = 27
			mAmount226 = mAmount224 - mAmount225
			.Text = VB6.Format(mAmount226, "0.00")
			
			.Col = 28
			mAmount227 = Val(.Text)
			
			.Col = 29
			mAmount228 = Val(.Text)
			
			.Col = 30
			mAmount229 = Val(.Text)
			
			.Col = 31
			mAmount230 = mAmount227 + mAmount228 + mAmount229
			.Text = VB6.Format(mAmount230, "0.00")
			
			.Col = 32
			mAmount231 = mAmount226 - mAmount230
			.Text = VB6.Format(mAmount231, "0.00")
			
		End With
		Exit Sub
ErrPart1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetAmountFromDetail(ByRef mCompanyCode As Integer, ByRef mFYear As Integer, ByRef mEmpCode As String, ByRef mRow As Integer, ByRef pCalcField As String) As Double
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTempDetail As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAmount As Double
		
		GetAmountFromDetail = 0
		mRow = IIf(RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12 And mRow > 10, mRow + 7, mRow)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = " Select " & pCalcField & " AS TOTALAMOUNT " & vbCrLf & " FROM PAY_ITCOMP_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND SUBROWNO=" & mRow & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTempDetail.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			GetAmountFromDetail = CDbl(VB6.Format(IIf(IsDbNull(RsTempDetail.Fields("TOTALAMOUNT").Value), "", RsTempDetail.Fields("TOTALAMOUNT").Value), "0.00"))
		End If
		Exit Function
ErrPart1: 
		GetAmountFromDetail = 0
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetAmountFromDetail12BA(ByRef mCompanyCode As Integer, ByRef mFYear As Integer, ByRef mEmpCode As String, ByRef mRow As Integer) As Double
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTempDetail As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAmount As Double
		
		GetAmountFromDetail12BA = 0
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = " Select AMOUNT3 AS TOTALAMOUNT " & vbCrLf & " FROM PAY_ITFORM12BA_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & mCompanyCode & "" & vbCrLf & " AND FYEAR=" & mFYear & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'" & vbCrLf & " AND SUBROW=" & mRow & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDetail, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTempDetail.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			GetAmountFromDetail12BA = CDbl(VB6.Format(IIf(IsDbNull(RsTempDetail.Fields("TOTALAMOUNT").Value), "", RsTempDetail.Fields("TOTALAMOUNT").Value), "0.00"))
		End If
		Exit Function
ErrPart1: 
		GetAmountFromDetail12BA = 0
	End Function
	
	Private Function GetEMPSNoFROM24(ByRef mCompanyCode As Integer, ByRef mEmpCode As String) As Integer
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim cntRow As Integer
		Dim pEmpCode As String
		Dim pCompanyCode As String
		
		GetEMPSNoFROM24 = 0
		
		With SprdView24
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 34
				pEmpCode = Trim(.Text)
				
				.Col = 35
				pCompanyCode = Trim(.Text)
				
				If mEmpCode = pEmpCode And mCompanyCode = CDbl(pCompanyCode) Then
					GetEMPSNoFROM24 = cntRow
					Exit Function
				End If
			Next 
		End With
		Exit Function
ErrPart1: 
		GetEMPSNoFROM24 = False
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
		Call FormatSprdView24()
		Call FormatSprdViewChallan()
		Call FormatSprdViewAnnex()
	End Sub
	
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView24()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdView24
			.MaxCols = 35
			
			.set_RowHeight(0, RowHeight * 7)
			
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
			.set_ColWidth(.Col, 20)
			.ColsFrozen = 3
			
			.Col = 4
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 5
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			For i = 6 To 32
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
			
			.Col = 33
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 7)
			
			.Col = 34
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 7)
			
			.Col = 35
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			FillHeadingSprdView24()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdView24, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdView24, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex
			.MaxCols = 18
			
			.set_RowHeight(0, RowHeight * 3.5)
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 25)
			.ColsFrozen = 1
			
			.Col = 2
			.CellType = SS_CELL_TYPE_INTEGER
			.TypeHAlign = SS_CELL_H_ALIGN_RIGHT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 6)
			
			For i = 3 To 16
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
			
			.Col = 17
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			.Col = 18
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			
			FillHeadingSprdViewAnnex()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewAnnex, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewAnnex, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewChallan()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewChallan
			.MaxCols = 13
			
			.set_RowHeight(0, RowHeight * 5)
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 6)
			
			For i = 2 To 7
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
			
			For i = 8 To 13
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 10)
			Next 
			
			FillHeadingSprdViewChallan()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewChallan, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewChallan, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	
	Private Sub FillHeadingSprdViewAnnex()
		
		With SprdViewAnnex
			.Row = 0
			
			.Col = 1
			.Text = "Name of Employee" & vbNewLine & "(245)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Employee's Serial No. in column 201 of Form No. 24" & vbNewLine & "(246)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Where accomodation is unfurnished" & vbNewLine & "(247)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Value as if accomodation is unfurnished" & vbNewLine & "(248)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Cost of furniture (including TV sets, radio sets, refrigerators, other house hold appliances and air-condioning plant or equipment)" & vbNewLine & "(249)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Perquisite value of furniture (10% of Columns 249)" & vbNewLine & "(250)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total of Columns 248 and 250" & vbNewLine & "(251)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Rent, if any, paid by the employee" & vbNewLine & "(252)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Value of perquisites (Columns 247 minus Columns 252 or Columns 251 minus Columns 252 as may be applicable)" & vbNewLine & "(253)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Where any conveyance has been provided by the employer free or at a concessional rate or where the employee is allowed the use of one or more motor-cars owned or hired by the employer or where the employer incurs the running expenses of a motor var owned by employees estimated values of perquisites (give details)" & vbNewLine & "(254)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Remunseration paid by the employer for domestic and / personal servies provided to the employee (give details)" & vbNewLine & "(255)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Values of free or concessional passages on home leave and other travelling to the extent chargeable to tax (give details)" & vbNewLine & "(256)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Estimated value of any other benefit or amenity provided by the employer free of cost or at concessional rate not included in the preceding Columns (Give Detail)" & vbNewLine & "(257)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Employer's Contribution to recognised provident fund in excess of 12% of the employee's salary" & vbNewLine & "(258)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Interest Credited to the assessee's account in recognised provident fund in excess of the rate fixed by crntral Goverment" & vbNewLine & "(259)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Total of Columns 253 to 259 carried to column 207 of Form no. 24" & vbNewLine & "(260)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "Emp Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			
		End With
	End Sub
	
	Private Sub FillHeadingSprdViewChallan()
		
		With SprdViewChallan
			.Row = 0
			
			.Col = 1
			.Text = "S. No." & vbNewLine & "(233)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "TDS Rs." & vbNewLine & "(234)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Surcharges Rs." & vbNewLine & "(235)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Education Cess Rs." & vbNewLine & "(236)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Interest Rs." & vbNewLine & "(237)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Others Rs." & vbNewLine & "(238)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total tax deposited Rs. (234 + 235 + 236 + 237 + 238)" & vbNewLine & "(239)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Cheque / DD No. (is any)" & vbNewLine & "(240)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "BSR Code" & vbNewLine & "(241)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Date on which tax deposited" & vbNewLine & "(242)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Transder voucher / Challan Serial no." & vbNewLine & "(243)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Whether TDS Deposited by Book Entry ? (Yes / No)" & vbNewLine & "(244)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	
	Private Sub FillHeadingSprdView24()
		
		With SprdView24
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(201)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "PAN No" & vbNewLine & "(202)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Name of the Employee" & vbNewLine & "(203)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Date From" & vbNewLine & "(204)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Date To" & vbNewLine & "(204)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Total amount of salary, excluding amount required to be shown in columns 206 and 207 (see Note 4)" & vbNewLine & "(205)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total amount of House rent allowance and other allowance to the extent chargeable to tax [see section 10(13A) read with rule 2A and section 10(14)" & vbNewLine & "(206)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Value of perquisites and amount of accretion to Employee's Provident Fund Account as per Annexure" & vbNewLine & "(207)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Amount of allowances and perquisites claimed as exempt and not included in cloumns 206 and 207" & vbNewLine & "(208)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Total of columns 205, 206,207" & vbNewLine & "(209)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Total deductions under section 16(i), 16(ii) and 16(iii) (specify each deduction separately)" & vbNewLine & "(210)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Income chargeable under the head 'Salaries' (Column 209 minus 210)" & vbNewLine & "(211)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Income (inculding loss from house property) under any head other than income under the head 'salaries' offered for TDS [section 192(2B)]" & vbNewLine & "(212)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Gross total income (Total of columns 211 and 212)" & vbNewLine & "(213)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Amount deductible under section 80G in respect of donations to certain funds, charitable institutions" & vbNewLine & "(214)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Amount deductible under section section 80GG in respect of rents paid" & vbNewLine & "(215)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "Amount deductible under any other provision of chapter VIA (indicate relevant section and amount deucted)" & vbNewLine & "(216)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "Total amount deductible under Chapter VIA (Total of columns 214,215 and 216)" & vbNewLine & "(217)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 19
			.Text = "Total taxable income (Columns 213 minus Columns 217)" & vbNewLine & "(218)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 20
			.Text = "Income-tax on Total income" & vbNewLine & "(219)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 21
			.Text = "Income-tax rebate under section 88 on life insurance premium, contribution to provident fund, etc. [See note 5]" & vbNewLine & "(220)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 22
			.Text = "Income-tax rebate under section 88B" & vbNewLine & "(221)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 23
			.Text = "Income-tax Rebate under section 88C" & vbNewLine & "(222)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 24
			.Text = "Income-tax Rebate under section 88D" & vbNewLine & "(223)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 25
			.Text = "Total Income-tax payable (columns 219 minus total of columns 220,221,222 and 223) including surcharge and education cess" & vbNewLine & "(224)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 26
			.Text = "Income-tax relief under section 89, when salary, etc. is paid in arrears or in advance" & vbNewLine & "(225)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 27
			.Text = "Net tax payable (Column 224 minus column 225)" & vbNewLine & "(226)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 28
			.Text = "Tax deducted at source Income-tax" & vbNewLine & "(227)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 29
			.Text = "Surcharge" & vbNewLine & "(228)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 30
			.Text = "Education Cess" & vbNewLine & "(229)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 31
			.Text = "Total income-tax deducted at source (Total of columns 227, 228 and 229)" & vbNewLine & "(230)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 32
			.Text = "Tax payable / refunable (Difference of Columns 226 and 230)" & vbNewLine & "(231)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 33
			.Text = "Remarks (See Notes 6 and 7)" & vbNewLine & "(232)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 34
			.Text = "Employee Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 35
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub frmTDSeReturn24_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		FormActive = False
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		Dim mMonthType As String
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtTDSAcNo.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPanNo.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		txtTDSAcNo.Enabled = False
		txtPanNo.Enabled = False
		optAddressChange(1).Checked = True
		optResAddChanged(1).Checked = True
		
		txtPersonName.Text = RsCompany.Fields("COMPANY_NAME").Value
		txtDeductorType.Text = "Others"
		txtDeductorType.Enabled = False
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtDesg.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
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
		
		txtRundate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdView24, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewChallan, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewAnnex, RowHeight)
		
	End Sub
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
		'UPGRADE_WARNING: TextBox property txtDesg.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDesg.Maxlength = 20
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
		txtPhone.Maxlength = 25
		'UPGRADE_WARNING: TextBox property txtEmail.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtEmail.Maxlength = 25
		
		
		'UPGRADE_WARNING: TextBox property txtPersonName_p.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPersonName_p.Maxlength = 75
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
		txtPhone_p.Maxlength = 25
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
		
		pFileName = mPubTDSPath & "\eReturn24.txt"
		
		FilePath = ""
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePath = Dir(mPubTDSPath, FileAttribute.Directory) ''Dir(pFileName)
		
		If FilePath = "" Then
			Call MkDir(mPubTDSPath)
		End If
		
		
		Call ShellAndContinue("ATTRIB +A -R " & pFileName)
		FileOpen(1, pFileName, OpenMode.Output)
		mLineCount = 1
		
		Call PrintFH(mLineCount)
		Call PrintBH(mLineCount)
		Call PrintCD(mLineCount)
		Call PrintDD(mLineCount)
		Call PrintPD(mLineCount)
		
		
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
		
		
		mString = VB6.Format(mLineCount, "000000000")
		mMainString = mString
		
		mString = "FH"
		mMainString = mMainString & mString
		
		mString = "SL3"
		mMainString = mMainString & mString
		
		mString = "R"
		mMainString = mMainString & mString
		
		mString = VB6.Format(txtRundate.Text, "DDMMYYYY")
		mMainString = mMainString & mString
		
		mString = VB6.Format(mLineCount, "000000000")
		mMainString = mMainString & mString
		
		mString = Trim(txtTDSAcNo.Text) & New String(" ", 10 - Len(Trim(txtTDSAcNo.Text)))
		mMainString = mMainString & mString
		
		mString = VB6.Format(mLineCount, "000000000")
		mMainString = mMainString & mString
		
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
		Dim mRs As Double
		Dim mPaisa As Double
		
		With SprdViewChallan
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				
				'''1
				.Col = 1
				mString = VB6.Format(mLineCount, "000000000")
				mMainString = mString
				
				'''2
				mString = "CD"
				mMainString = mMainString & mString
				
				'''3
				mString = VB6.Format(1, "000000000")
				mMainString = mMainString & mString
				
				'''4
				mString = VB6.Format(cntRow, "000000000")
				mMainString = mMainString & mString
				
				'''5
				.Col = 2
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''6
				.Col = 3
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''7
				.Col = 4
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''8
				.Col = 5
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''9
				.Col = 6
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				
				'''10
				.Col = 7
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''11
				.Col = 8
				mString = CStr(Val(VB.Left(Trim(.Text), 14)))
				mString = VB6.Format(mString, "00000000000000")
				mMainString = mMainString & mString
				
				'''12
				.Col = 9
				mString = VB.Left(Trim(.Text), 7)
				mString = mString & New String(" ", 7 - Len(mString))
				mMainString = mMainString & mString
				
				'''13
				.Col = 10
				mString = VB6.Format(Trim(.Text), "DDMMYYYY")
				mMainString = mMainString & mString
				
				'''14
				.Col = 11
				mString = VB.Left(Trim(.Text), 9)
				mString = mString & New String(" ", 9 - Len(mString))
				mMainString = mMainString & mString
				
				''15
				.Col = 12
				mString = VB.Left(Trim(.Text), 1)
				mMainString = mMainString & mString
				
				PrintLine(1, TAB(0), mMainString)
				
				mLineCount = mLineCount + 1
				
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
		Dim mRs As Double
		Dim mPaisa As Double
		Dim mCntRow As Integer
		Dim mTotChallanNo As Double
		Dim mTotDeductee As Double
		Dim mChallanAmount As Double
		Dim mDeducteeAmount As Double
		Dim mTotPerquisiteRecd As Double
		Dim mAmountPaid As Double
		
		mCntRow = 1
		If GetChallanDetail(mTotChallanNo, mTotDeductee, mTotPerquisiteRecd, mChallanAmount, mDeducteeAmount, mAmountPaid) = False Then GoTo ErrPart
		
		'''1
		mString = VB6.Format(mLineCount, "000000000")
		mMainString = mString
		
		'''2
		mString = "BH"
		mMainString = mMainString & mString
		
		'''3
		mString = VB6.Format(mCntRow, "000000000")
		mMainString = mMainString & mString
		
		'''4
		mString = VB6.Format(mTotChallanNo, "0")
		mString = New String("0", 9 - Len(mString)) & mString
		mMainString = mMainString & mString
		
		''' 5
		mString = VB6.Format(mTotDeductee, "0")
		mString = New String("0", 9 - Len(mString)) & mString
		mMainString = mMainString & mString
		
		''' 6
		mString = VB6.Format(mTotPerquisiteRecd, "0")
		mString = New String("0", 9 - Len(mString)) & mString
		mMainString = mMainString & mString
		
		'''7
		mString = New String(" ", 8)
		mMainString = mMainString & mString
		
		'''8
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mString = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		mString = mString & New String(" ", 10 - Len(mString))
		mMainString = mMainString & mString
		
		''' 9
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mString = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		mString = mString & New String(" ", 10 - Len(mString))
		mMainString = mMainString & mString
		
		''' 10
		mString = VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000") & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, RsCompany.Fields("END_DATE").Value), "YY")
		mMainString = mMainString & mString
		
		''' 11
		mString = VB6.Format(Year(RsCompany.Fields("START_DATE").Value), "0000") & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
		mMainString = mMainString & mString
		
		''' 12
		mString = UCase(Trim(txtPersonName_p.Text))
		mString = mString & New String(" ", 75 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''13
		mString = "0000000002"
		mMainString = mMainString & mString
		
		''' 14
		mString = VB.Left(UCase(Trim(txtFlat.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 15
		mString = VB.Left(UCase(Trim(txtBuilding.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 16
		mString = VB.Left(UCase(Trim(txtRoad.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 17
		mString = VB.Left(UCase(Trim(txtArea.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 18
		mString = VB.Left(UCase(Trim(txtTown.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		
		''' 19
		mString = GetStateCode_TDS((txtState.Text))
		mString = VB6.Format(mString, "00")
		mMainString = mMainString & mString
		
		''' 20
		mString = VB.Left(UCase(Trim(txtPinCode.Text)), 6)
		mString = VB6.Format(mString, "000000")
		mMainString = mMainString & mString
		
		''' 21
		mString = IIf(optAddressChange(0).Checked = True, "Y", "N")
		mString = mString & New String(" ", 1 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 22
		mString = VB.Left(UCase(Trim(txtPersonName_p.Text)), 75)
		mString = mString & New String(" ", 75 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 23
		mString = VB.Left(UCase(Trim(txtDesg.Text)), 20)
		mString = mString & New String(" ", 20 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 24
		mString = VB.Left(UCase(Trim(txtFlat_p.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 25
		mString = VB.Left(UCase(Trim(txtBuilding_p.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 26
		mString = VB.Left(UCase(Trim(txtRoad_p.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 27
		mString = VB.Left(UCase(Trim(txtArea_p.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 28
		mString = VB.Left(UCase(Trim(txtTown_p.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		
		''' 29
		mString = GetStateCode_TDS((txtState_p.Text))
		mString = VB6.Format(mString, "00")
		mMainString = mMainString & mString
		
		''' 30
		mString = VB.Left(UCase(Trim(txtPinCode_p.Text)), 6)
		mString = VB6.Format(mString, "000000")
		mMainString = mMainString & mString
		
		''' 31
		mString = IIf(optResAddChanged(0).Checked = True, "Y", "N")
		mString = mString & New String(" ", 1 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 32
		mString = VB6.Format(mChallanAmount, "0.00")
		mRs = CDbl(Mid(Trim(mString), 1, InStr(1, Trim(mString), ".") - 1))
		mPaisa = CDbl(Mid(Trim(mString), InStr(1, Trim(mString), ".") + 1))
		mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
		mMainString = mMainString & mString
		
		''' 33
		mString = VB6.Format(mDeducteeAmount, "0.00")
		mRs = CDbl(Mid(Trim(mString), 1, InStr(1, Trim(mString), ".") - 1))
		mPaisa = CDbl(Mid(Trim(mString), InStr(1, Trim(mString), ".") + 1))
		mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
		mMainString = mMainString & mString
		
		'''34
		mString = VB6.Format(0, "00000000000000")
		mMainString = mMainString & mString
		
		'''35
		mString = New String(" ", 10)
		mMainString = mMainString & mString
		
		'''36
		mString = VB6.Format(0, "00000000000000")
		mMainString = mMainString & mString
		
		'''37
		mString = VB6.Format(0, "00000000000000")
		mMainString = mMainString & mString
		
		PrintLine(1, TAB(0), mMainString)
		
		mLineCount = mLineCount + 1
		
		PrintBH = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintBH = False
		'    Resume
	End Function
	Private Function GetChallanDetail(ByRef pTotChallanNo As Double, ByRef pTotDeductee As Double, ByRef pTotPerquisiteRecd As Double, ByRef pChallanAmount As Double, ByRef pDeducteeAmount As Double, ByRef pAmountPaid As Double) As Boolean
		On Error GoTo ErrPart1
		Dim cntRow As Integer
		
		pTotChallanNo = 0
		pTotDeductee = 0
		pTotPerquisiteRecd = 0
		pChallanAmount = 0
		pDeducteeAmount = 0
		pAmountPaid = 0
		
		pTotChallanNo = SprdViewChallan.MaxRows
		pTotDeductee = SprdView24.MaxRows
		pTotPerquisiteRecd = SprdViewAnnex.MaxRows
		
		With SprdViewChallan
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 7
				pChallanAmount = pChallanAmount + Val(.Text)
				
			Next 
		End With
		
		With SprdView24
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 19
				pAmountPaid = pAmountPaid + Val(.Text)
				
				.Col = 31
				pDeducteeAmount = pDeducteeAmount + Val(.Text)
			Next 
		End With
		
		GetChallanDetail = True
		Exit Function
ErrPart1: 
		GetChallanDetail = False
	End Function
	Private Function PrintDD(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim i As Integer
		
		mString = ""
		With SprdView24
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				
				'''1
				mString = VB6.Format(mLineCount, "000000000")
				mMainString = mString
				
				'''2
				mString = "DD"
				mMainString = mMainString & mString
				
				'''''3
				mString = VB6.Format(1, "000000000")
				mMainString = mMainString & mString
				
				'''4
				mString = VB6.Format(cntRow, "000000000")
				mMainString = mMainString & mString
				
				'''5
				.Col = 1
				mString = VB6.Format(Trim(.Text), "000000000")
				mMainString = mMainString & mString
				
				''6
				.Col = 2
				If Len(Trim(.Text)) <> 10 Then
					mString = ""
				Else
					mString = VB.Left(UCase(Trim(.Text)), 10)
				End If
				
				mString = Trim(mString) & New String(" ", 10 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				''7
				.Col = 3
				mString = VB.Left(UCase(Trim(.Text)), 75)
				mString = Trim(mString) & New String(" ", 75 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''8
				.Col = 4
				mString = VB6.Format(.Text, "DDMMYYYY")
				mString = Trim(mString) & New String(" ", 8 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''9
				.Col = 5
				mString = VB6.Format(.Text, "DDMMYYYY")
				mString = Trim(mString) & New String(" ", 8 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''10 to 16
				For i = 6 To 12
					.Col = i
					mRs = 0
					mPaisa = 0
					If Val(.Text) <> 0 Then
						mRs = System.Math.Abs(CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1)))
						mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
					End If
					
					mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
					mMainString = mMainString & mString
				Next 
				
				'''17
				.Col = 13
				mString = IIf(Val(.Text) >= 0, "P", "N")
				mMainString = mMainString & mString
				
				'''18 to 36
				For i = 13 To 31
					.Col = i
					mRs = 0
					mPaisa = 0
					If Trim(.Text) <> "" Then
						mRs = System.Math.Abs(CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1)))
						mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
					End If
					
					If i = 13 Then
						mString = VB6.Format(Val(CStr(mRs)), "00000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
					Else
						mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
					End If
					mMainString = mMainString & mString
				Next 
				
				'''37
				.Col = 32
				mString = IIf(Val(.Text) >= 0, "P", "N")
				mMainString = mMainString & mString
				
				'''38
				.Col = 32
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = System.Math.Abs(CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1)))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				
				mString = VB6.Format(Val(CStr(mRs)), "00000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				
				'''39
				mString = ""
				mString = Trim(mString) & New String(" ", 75 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				PrintLine(1, TAB(0), mMainString)
				
				mLineCount = mLineCount + 1
				
			Next 
		End With
		PrintDD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintDD = False
		'    Resume
	End Function
	Private Function PrintPD(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim i As Integer
		
		With SprdViewAnnex
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				
				'''1
				mString = VB6.Format(mLineCount, "000000000")
				mMainString = mString
				
				'''2
				mString = "PD"
				mMainString = mMainString & mString
				
				'''''3
				mString = VB6.Format(1, "000000000")
				mMainString = mMainString & mString
				
				'''4
				mString = VB6.Format(cntRow, "000000000")
				mMainString = mMainString & mString
				
				''5
				.Col = 1
				mString = VB.Left(UCase(Trim(.Text)), 75)
				mString = Trim(mString) & New String(" ", 75 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''6
				.Col = 2
				mString = VB6.Format(Trim(.Text), "000000000")
				mString = mString
				mMainString = mMainString & mString
				
				'''7 to 20
				For i = 3 To 16
					.Col = i
					mRs = 0
					mPaisa = 0
					If Trim(.Text) <> "" Then
						mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
						mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
					End If
					
					mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
					mMainString = mMainString & mString
				Next 
				
				PrintLine(1, TAB(0), mMainString)
				
				mLineCount = mLineCount + 1
				
			Next 
		End With
		PrintPD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintPD = False
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