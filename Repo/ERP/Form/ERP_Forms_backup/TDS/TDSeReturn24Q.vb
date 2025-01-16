Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTDSeReturn24Q
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
		frmPrintTDS.OptForm26.Text = "Form 24Q"
		frmPrintTDS.OptFormChallan.Text = "Form 24Q (Challan)"
		frmPrintTDS.OptFormChallan.Enabled = False
		frmPrintTDS.OptAnnexure2.Enabled = True
		frmPrintTDS.OptAnnexure3.Enabled = True
		frmPrintTDS.fraAnnx.Enabled = False
		
		frmPrintTDS.ShowDialog()
		
		If G_PrintLedg = False Then
			Exit Sub
		End If
		
		Call InsertIntoPrintDummy()
		
		If frmPrintTDS.OptForm26.Checked = True Then
			mTitle = "Form No. 24Q"
			mSubTitle = "[See section 192 and rule 37]"
			
			mReportFileName = "TDSeReturn24Q.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		ElseIf frmPrintTDS.OptForm27A.Checked = True Then 
			
			mTitle = "Form No. 27A"
			mSubTitle = "[See rule 37B"
			
			mReportFileName = "TDSeReturn27A.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			mTitle = "A N N E X U R E I"
			mSubTitle = "Deductee-wise break-up of TDS"
			
			mReportFileName = "TDSeReturn24QAnnex_I.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		ElseIf frmPrintTDS.OptAnnexure2.Checked = True Then 
			mTitle = "A N N E X U R E II"
			mSubTitle = ""
			
			mReportFileName = "TDSeReturn24QAnnex_II.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
		ElseIf frmPrintTDS.OptAnnexure3.Checked = True Then 
			mTitle = "A N N E X U R E III"
			mSubTitle = ""
			
			mReportFileName = "TDSeReturn24QAnnx_III.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)
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
			If InsertGridDetail(SprdViewChallan, 2, (SprdViewChallan.MaxRows), (SprdViewChallan.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			If InsertGridDetail(SprdViewAnnex1, 1, (SprdViewAnnex1.MaxRows), (SprdViewAnnex1.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptAnnexure2.Checked = True Then 
			If InsertGridDetail(SprdViewAnnex2, 1, (SprdViewAnnex2.MaxRows), (SprdViewAnnex2.MaxCols)) = False Then GoTo ERR1
		ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
			If InsertGridDetail(SprdViewAnnex3, 1, (SprdViewAnnex3.MaxRows), (SprdViewAnnex3.MaxCols)) = False Then GoTo ERR1
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
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
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
		
		If frmPrintTDS.OptForm26.Checked = True Or frmPrintTDS.OptForm27A.Checked = True Or frmPrintTDS.OptAnnexure.Checked = True Then
			If frmPrintTDS.OptForm26.Checked = True Then
				mFormTitle = "Quarterly statement of deduction of tax under sub-section (3) of section 200 of the Income-tax Act, 1961 in respect of Salary for the quarter ended " & VB6.Format(txtDateTo.Text, "MMMM-YYYY")
				
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
			ElseIf frmPrintTDS.OptAnnexure.Checked = True Then 
				mFormTitle = "Please use separate Annexure for each line - item in the table at S.No. 04 of main Form 24Q"
			End If
			
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
		ElseIf frmPrintTDS.OptAnnexure3.Checked = True Then 
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
		Dim mFP As Boolean
		'    mFP = Shell(mLocalPath & "\TDS_FVU.bat", vbNormalFocus)
		Shell(My.Application.Info.DirectoryPath & "\TDS_FVU.bat")
	End Sub
	
	'UPGRADE_WARNING: Form event frmTDSeReturn24Q.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Public Sub frmTDSeReturn24Q_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
	Private Sub frmTDSeReturn24Q_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
		Me.Height = VB6.TwipsToPixelsY(7245)
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
		
		
		If ShowDetailChallan = False Then GoTo ErrPart
		If ShowDetailAnnex1 = False Then GoTo ErrPart
		If ShowDetailAnnex2 = False Then GoTo ErrPart
		If ShowDetailAnnex3 = False Then GoTo ErrPart
		FormatSprdView()
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SortGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Call MainClass.SortGrid(SprdViewAnnex2, 10, 3, True, False)
		
		With SprdViewAnnex2
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 1
				.Text = VB6.Format(cntRow, "0")
			Next 
		End With
		
		With SprdViewAnnex3
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
		Call MainClass.SortGrid(SprdViewAnnex3, 2, 1, False, False)
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgInformation(Err.Description)
		
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetailAnnex3() As Boolean
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
		
		With SprdViewAnnex3
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
		ShowDetailAnnex3 = True
		Exit Function
ErrPart1: 
		ShowDetailAnnex3 = False
		'    Resume
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function ShowDetailAnnex1() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAmount318 As Double
		Dim mAmount319 As Double
		Dim mAmount320 As Double
		Dim mAmount321 As Double
		Dim mAmount322 As Double
		Dim mAmount323 As Double
		Dim mPANNo As String
		
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
		Dim mTaxableAmount As Double
		Dim mCntEmpChallan As Integer
		
		SqlStr = " Select IH.AUTO_KEY_REFNO,IH.COMPANY_CODE, IH.CHQ_DATE, IH.VDATE, IH.CHALLANDATE, ID.*, " & vbCrLf & " ID.EMP_CODE, EMP.EMP_PANNO, EMP.EMP_NAME "
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND ID.EMP_CODE=EMP.EMP_CODE"
		
		SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND IH.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdViewAnnex1
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					
					.Row = cntRow
					.Col = 1
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Trim(mPrevChallanMkey) = Trim(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value)) Then
						mChallanWiseSNo = mChallanWiseSNo + 1
					Else
						mChallanWiseSNo = 1
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						mChallanMkey = Trim(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))
						mChallanSNo = GetChallanSNO(mChallanMkey, mSectionCode, mBSRCode, mDepositDate, mChallanNo, mTotalTDS, mTotalInerest, mOtherAmt, mTotalTaxDeposit)
					End If
					
					.Text = CStr(mChallanWiseSNo) '''cntRow
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mPANNo = IIf(IsDbNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
					If Len(mPANNo) = 10 Then
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Text = IIf(IsDbNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
					ElseIf UCase(Trim(mPANNo)) = "A/F" Then 
						.Text = "PANAPPLIED"
					ElseIf Trim(mPANNo) = "" Then 
						.Text = "PANNOTAVBL"
					Else
						.Text = "PANINVALID"
					End If
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CHQ_DATE").Value), "", RsTemp.Fields("CHQ_DATE").Value), "DD/MM/YYYY")
					
					.Col = 6
					mTaxableAmount = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsCompany.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 61, "TOTALAMOUNT")
					mTaxableAmount = System.Math.Round(mTaxableAmount / 12, 0)
					'                mCntEmpChallan = GetEmpChallanNo(RsTemp!EMP_CODE, RsTemp!COMPANY_CODE)
					'                If mCntEmpChallan <> 0 Then
					'                    mTaxableAmount = Round(mTaxableAmount * 3 / mCntEmpChallan, 0)
					'                End If
					'
					.Text = VB6.Format(mTaxableAmount, "0.00")
					
					.Col = 7
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TDS_AMOUNT").Value), "", RsTemp.Fields("TDS_AMOUNT").Value), "0.00")
					
					.Col = 8
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SURCHARGE_AMT").Value), "", RsTemp.Fields("SURCHARGE_AMT").Value), "0.00")
					
					.Col = 9
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CESS_AMT").Value), "", RsTemp.Fields("CESS_AMT").Value), "0.00")
					
					.Col = 10
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value), "0.00")
					
					.Col = 11
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("Amount").Value), "", RsTemp.Fields("Amount").Value), "0.00")
					
					.Col = 12
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
					
					.Col = 13
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")
					
					.Col = 14
					.Text = ""
					
					.Col = 15
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = Str(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
					.Col = 16
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = Str(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mPrevChallanMkey = Str(IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value))
					
					.Col = 17
					.Text = Trim(mBSRCode)
					
					.Col = 18
					.Text = Trim(mDepositDate)
					
					.Col = 19
					.Text = Trim(mChallanNo)
					
					.Col = 20
					.Text = Trim(mSectionCode)
					
					.Col = 21
					.Text = VB6.Format(mTotalTDS, "0.00")
					
					.Col = 22
					.Text = VB6.Format(mTotalInerest, "0.00")
					
					.Col = 23
					.Text = VB6.Format(mOtherAmt, "0.00")
					
					.Col = 24
					.Text = VB6.Format(mTotalTaxDeposit, "0.00")
NextRec: 
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						cntRow = cntRow + 1
						.MaxRows = cntRow
					End If
				Loop 
			End If
		End With
		ShowDetailAnnex1 = True
		Exit Function
ErrPart1: 
		ShowDetailAnnex1 = False
		'    Resume
	End Function
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetEmpChallanNo(ByRef pEmpCode As String, ByRef pCompany_Code As Integer) As Integer
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim pCntEmpChallan As Integer
		
		pCntEmpChallan = 0
		SqlStr = " Select Count(ID.EMP_CODE) AS CNT"
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO "
		
		SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND IH.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND ID.EMP_CODE='" & pEmpCode & "'"
		
		SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		'    SqlStr = SqlStr & vbCrLf _
		''        & " ORDER BY IH.COMPANY_CODE, IH.AUTO_KEY_REFNO,EMP.EMP_NAME"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pCntEmpChallan = CInt(Trim(IIf(IsDbNull(RsTemp.Fields("CNT").Value), 0, RsTemp.Fields("CNT").Value)))
		End If
		GetEmpChallanNo = pCntEmpChallan
		Exit Function
ErrPart1: 
		pCntEmpChallan = 0
	End Function
	Private Function GetChallanSNO(ByRef pChallanMKey As String, ByRef pSectionCode As String, ByRef pBSRCode As String, ByRef pDepositDate As String, ByRef pChallanNo As String, ByRef pTotalTDS As Double, ByRef pTotalInerest As Double, ByRef pOtherAmt As Double, ByRef pTotalTaxDeposit As Double) As Integer
		On Error GoTo ErrPart1
		Dim cntRow As Integer
		
		GetChallanSNO = 0
		With SprdViewChallan
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 15
				If Trim(.Text) = Trim(pChallanMKey) Then
					GetChallanSNO = cntRow
					'                .Col = 2
					pSectionCode = "92B"
					
					.Col = 2
					pTotalTDS = Val(.Text)
					
					.Col = 3
					pTotalTDS = pTotalTDS + Val(.Text)
					
					.Col = 4
					pTotalTDS = pTotalTDS + Val(.Text)
					
					.Col = 5
					pTotalInerest = Val(.Text)
					
					.Col = 6
					pOtherAmt = Val(.Text)
					
					.Col = 7
					pTotalTaxDeposit = Val(.Text)
					
					.Col = 9
					pBSRCode = Trim(.Text)
					
					.Col = 10
					pDepositDate = Trim(.Text)
					
					.Col = 11
					pChallanNo = Trim(.Text)
					
					Exit For
				End If
			Next 
		End With
		
		
		
		Exit Function
ErrPart1: 
		
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
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY AUTO_KEY_REFNO,CHALLANDATE,COMPANY_CODE"
		
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
					.Text = Str(RsTemp.Fields("COMPANY_CODE").Value)
					
					.Col = 14
					.Text = Str(RsTemp.Fields("AUTO_KEY_REFNO").Value)
					
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
	Private Function ShowDetailAnnex2() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart1
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		Dim cntRow As Integer
		Dim mAmount331 As Double
		Dim mAmount332 As Double
		Dim mAmount333 As Double
		Dim mAmount334 As Double
		Dim mAmount335 As Double
		Dim mAmount336 As Double
		Dim mAmount337 As Double
		Dim mAmount338 As Double
		Dim mAmount339 As Double
		Dim mAmount340 As Double
		Dim mAmount341 As Double
		Dim mAmount342 As Double
		Dim mAmount343 As Double
		Dim mAmount344 As Double
		Dim mAmount345 As Double
		Dim mAmount346 As Double
		Dim mAmount347 As Double
		Dim mAmount348 As Double
		Dim mAmount349 As Double
		Dim mAmount350 As Double
		Dim mAmount351 As Double
		Dim mAmount352 As Double
		Dim mPANNo As String
		Dim mTaxAmount As Double
		
		SqlStr = " Select IH.*, " & vbCrLf & " EMP.EMP_PANNO,EMP.EMP_NAME,EMP_SEX "
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCOMP_HDR IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=EMP.COMPANY_CODE" & vbCrLf & " AND IH.EMP_CODE=EMP.EMP_CODE"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " AND EMP_DOJ <=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "')"
		SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "') OR EMP_LEAVE_DATE IS NULL)"
		
		'    SqlStr = SqlStr & vbCrLf _
		''            & " AND IH.COMPANY_CODE || IH.FYEAR || IH.EMP_CODE IN ( " & vbCrLf _
		''            & " SELECT COMPANY_CODE || FYEAR || EMP_CODE " & vbCrLf _
		''            & " FROM PAY_ITCOMP_TRN " & vbCrLf _
		''            & " WHERE SUBROWNO=61 " & vbCrLf _
		''            & " AND TOTALAMOUNT>100000" & vbCrLf _
		''            & " AND FYEAR=" & RsCompany!FYEAR & ""
		'
		'    If chkConsolidated.Value = vbUnchecked Then
		'       SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany!COMPANY_CODE & ""
		'    End If
		'
		'    SqlStr = SqlStr & vbCrLf & ")"
		
		'    SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE='000999'"
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY EMP.EMP_NAME,IH.COMPANY_CODE "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdViewAnnex2
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					
					
					mTaxAmount = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 71, "TOTALAMOUNT")
					
					If mTaxAmount = 0 Then GoTo NextRec
					
					.MaxRows = cntRow
					.Row = cntRow
					.Col = 1
					.Text = CStr(cntRow)
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mPANNo = IIf(IsDbNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
					If Len(mPANNo) = 10 Then
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Text = IIf(IsDbNull(RsTemp.Fields("EMP_PANNO").Value), "", RsTemp.Fields("EMP_PANNO").Value)
					ElseIf UCase(Trim(mPANNo)) = "A/F" Then 
						.Text = "PANAPPLIED"
					ElseIf Trim(mPANNo) = "" Then 
						.Text = "PANNOTAVBL"
					Else
						.Text = "PANINVALID"
					End If
					
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
					
					.Col = 4
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsCompany.Fields("START_DATE").Value), "", RsCompany.Fields("START_DATE").Value), "DD/MM/YYYY")
					'                .Text = Format(IIf(IsNull(RsTemp!FROMDATE), "", RsTemp!FROMDATE), "DD/MM/YYYY")
					
					.Col = 5
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value), "DD/MM/YYYY")
					
					.Col = 7
					mAmount332 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 2, "TOTALAMOUNT")
					mAmount332 = mAmount332 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 3, "TOTALAMOUNT")
					mAmount332 = mAmount332 + GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 4, "TOTALAMOUNT")
					mAmount332 = mAmount332 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 31, "AMOUNT4")
					mAmount332 = mAmount332 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 32, "AMOUNT4")
					mAmount332 = mAmount332 - GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 33, "AMOUNT4")
					'
					.Text = VB6.Format(mAmount332, "0.00")
					
					.Col = 8
					mAmount333 = GetAmountFromDetail12BA(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 25)
					.Text = VB6.Format(mAmount333, "0.00")
					
					.Col = 9
					'                mAmount334 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 25, "AMOUNT4")
					'                mAmount334 = mAmount334 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 26, "AMOUNT4")
					mAmount334 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 35, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount334, "0.00")
					
					.Col = 6
					mAmount331 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 36, "TOTALAMOUNT")
					mAmount331 = mAmount331 - mAmount332 - mAmount333
					.Text = VB6.Format(mAmount331, "0.00")
					
					.Col = 10
					mAmount335 = mAmount331 + mAmount332 + mAmount333
					.Text = VB6.Format(mAmount335, "0.00")
					
					.Col = 11
					mAmount336 = 0 ''GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 37, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount336, "0.00")
					
					.Col = 12
					mAmount337 = mAmount335 - mAmount336
					.Text = VB6.Format(mAmount337, "0.00")
					
					.Col = 13
					mAmount338 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 44, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount338, "0.00")
					
					.Col = 14
					mAmount339 = System.Math.Round(mAmount337 + mAmount338, 0)
					.Text = VB6.Format(mAmount339, "0.00")
					
					.Col = 15
					mAmount340 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 48, "AMOUNT2")
					.Text = VB6.Format(mAmount340, "0.00")
					
					.Col = 16
					mAmount341 = 0 '' GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 47, "TOTALAMOUNT")
					.Text = VB6.Format(mAmount341, "0.00")
					
					.Col = 17
					mAmount342 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 60, "TOTALAMOUNT")
					mAmount342 = mAmount342 - mAmount340
					.Text = VB6.Format(mAmount342, "0.00")
					
					.Col = 18
					mAmount343 = mAmount340 + mAmount341 + mAmount342
					.Text = VB6.Format(mAmount343, "0.00")
					
					.Col = 19
					mAmount344 = mAmount339 - mAmount343
					.Text = VB6.Format(System.Math.Round(mAmount344, 0), "0.00")
					
					.Col = 20
					'                mAmount229 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 73, "TOTALAMOUNT")
					'                mAmount228 = GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 66, "TOTALAMOUNT")
					'                mAmount345 = mAmount229 + mAmount228 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 54, "TOTALAMOUNT")
					mAmount345 = GetAmountFromDetail(RsTemp.Fields("COMPANY_CODE").Value, RsTemp.Fields("FYEAR").Value, RsTemp.Fields("EMP_CODE").Value, 71, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount345, 0), "0.00")
					
					.Col = 21
					mAmount346 = 0 'GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 72, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount346, 0), "0.00")
					
					.Col = 22
					mAmount347 = 0 '' GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 74, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount347, 0), "0.00")
					
					.Col = 23
					mAmount348 = 0 ''GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 75, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount348, 0), "0.00")
					
					.Col = 24
					mAmount349 = 0 'GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 76, "TOTALAMOUNT")
					.Text = VB6.Format(System.Math.Round(mAmount349, 0), "0.00")
					
					.Col = 25
					mAmount350 = mAmount345 - (mAmount346 + mAmount347 + mAmount348 + mAmount349)
					'                mAmount350 = mAmount350 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 68, "TOTALAMOUNT")
					'                mAmount350 = mAmount350 + GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 70, "TOTALAMOUNT")
					
					.Text = VB6.Format(System.Math.Round(mAmount350, 0), "0.00")
					
					.Col = 26
					mAmount351 = 0 ''GetAmountFromDetail(RsTemp!COMPANY_CODE, RsTemp!FYEAR, RsTemp!EMP_CODE, 1)
					.Text = VB6.Format(System.Math.Round(mAmount351, 0), "0.00")
					
					.Col = 27
					mAmount352 = mAmount350 - mAmount351
					.Text = VB6.Format(System.Math.Round(mAmount352, 0), "0.00")
					
					.Col = 28
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
					
					.Col = 29
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = CStr(IIf(IsDbNull(RsTemp.Fields("COMPANY_CODE").Value), "", RsTemp.Fields("COMPANY_CODE").Value))
					
NextRec: 
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						If mTaxAmount > 0 Then
							cntRow = cntRow + 1
						End If
					End If
				Loop 
			End If
		End With
		ShowDetailAnnex2 = True
		Exit Function
ErrPart1: 
		'    Resume
		ShowDetailAnnex2 = False
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
		
		With SprdViewAnnex2
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
		'    mRow = IIf(RsCompany!COMPANY_CODE = 2 And mRow > 10, mRow + 7, mRow)
		
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
		
		With SprdViewAnnex2
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 34
				pEmpCode = Trim(.Text)
				
				.Col = 35
				pCompanyCode = CStr(Val(.Text))
				
				If mEmpCode = pEmpCode And mCompanyCode = Val(pCompanyCode) Then
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
		Call FormatSprdViewChallan()
		Call FormatSprdViewAnnex1()
		Call FormatSprdViewAnnex2()
		Call FormatSprdViewAnnex3()
	End Sub
	
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex2()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex2
			.MaxCols = 29
			
			.set_RowHeight(0, RowHeight * 8)
			
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
			
			For i = 6 To 27
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
			
			'        .Col = 33
			'        .CellType = SS_CELL_TYPE_EDIT
			'        .TypeHAlign = SS_CELL_H_ALIGN_LEFT
			'        .TypeMaxEditLen = 255
			'        .ColWidth(.Col) = 7
			
			.Col = 28
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 7)
			
			.Col = 29
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			FillHeadingSprdViewAnnex2()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewAnnex2, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewAnnex2, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex1()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex1
			.MaxCols = 24
			
			.set_RowHeight(0, RowHeight * 5)
			
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
			.set_ColWidth(.Col, 10)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 10)
			.ColsFrozen = 3
			
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
			
			For i = 6 To 11
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
			
			.Col = 12
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 13
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 14
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 15
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 16
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			
			For i = 17 To 20
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 10)
			Next 
			
			For i = 21 To 24
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
			
			FillHeadingSprdViewAnnex1()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewAnnex1, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewAnnex1, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex3()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex3
			.MaxCols = 18
			
			.set_RowHeight(0, RowHeight * 5)
			
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
			
			FillHeadingSprdViewAnnex3()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewAnnex3, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewAnnex3, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewChallan()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewChallan
			.MaxCols = 14
			
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
			
			For i = 8 To 14
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
	
	
	Private Sub FillHeadingSprdViewAnnex3()
		
		With SprdViewAnnex3
			.Row = 0
			
			.Col = 1
			.Text = "Name of Employee" & vbNewLine & "(353)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Employee's Serial No. in column 327 of Form No. 24" & vbNewLine & "(354)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Where accomodation is unfurnished" & vbNewLine & "(355)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Value as if accomodation is unfurnished" & vbNewLine & "(356)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Cost of furniture (including TV sets, radio sets, refrigerators, other house hold appliances and air-condioning plant or equipment)" & vbNewLine & "(357)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Perquisite value of furniture (10% of Columns 357)" & vbNewLine & "(358)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total of Columns 356 and 358" & vbNewLine & "(359)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Rent, if any, paid by the employee" & vbNewLine & "(360)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Value of perquisites (Columns 355 minus Columns 360 or Columns 359 minus Columns 360 as may be applicable)" & vbNewLine & "(361)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Where any conveyance has been provided by the employer free or at a concessional rate or where the employee is allowed the use of one or more motor-cars owned or hired by the employer or where the employer incurs the running expenses of a motor var owned by employees estimated values of perquisites (give details)" & vbNewLine & "(362)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Remunseration paid by the employer for domestic and / personal servies provided to the employee (give details)" & vbNewLine & "(363)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Values of free or concessional passages on home leave and other travelling to the extent chargeable to tax (give details)" & vbNewLine & "(364)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Estimated value of any other benefit or amenity provided by the employer free of cost or at concessional rate not included in the preceding Columns (Give Detail)" & vbNewLine & "(365)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Employer's Contribution to recognised provident fund in excess of 12% of the employee's salary" & vbNewLine & "(366)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Interest Credited to the assessee's account in recognised provident fund in excess of the rate fixed by crntral Goverment" & vbNewLine & "(367)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Total of Columns 361 to 367 carried to column 333 of Form no. 24" & vbNewLine & "(368)"
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
			.Text = "S. No." & vbNewLine & "(301)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "TDS Rs." & vbNewLine & "(302)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Surcharges Rs." & vbNewLine & "(303)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Education Cess Rs." & vbNewLine & "(304)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Interest Rs." & vbNewLine & "(305)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Others Rs." & vbNewLine & "(306)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total tax deposited Rs. (302 + 303 + 304 + 305 + 306)" & vbNewLine & "(307)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Cheque / DD No. (is any)" & vbNewLine & "(308)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "BSR Code" & vbNewLine & "(309)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Date on which tax deposited" & vbNewLine & "(310)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Transder voucher / Challan Serial no." & vbNewLine & "(311)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Whether TDS Deposited by Book Entry ? (Yes / No)" & vbNewLine & "(312)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Challan Ref No"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	
	Private Sub FillHeadingSprdViewAnnex2()
		
		With SprdViewAnnex2
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(327)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "PAN No" & vbNewLine & "(328)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Name of the Employee" & vbNewLine & "(329)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Date From" & vbNewLine & "(329)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Date To" & vbNewLine & "(330)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Total amount of salary, excluding amount required to be shown in columns 332 and 333 (see Note 4)" & vbNewLine & "(331)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Total amount of House rent allowance and other allowance to the extent chargeable to tax [see section 10(13A) read with rule 2A and section 10(14)" & vbNewLine & "(332)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Value of perquisites and amount of accretion to Employee's Provident Fund Account as per Annexure" & vbNewLine & "(333)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Amount of allowances and perquisites claimed as exempt and not included in cloumns 332 and 333" & vbNewLine & "(334)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Total of columns 331, 332,333" & vbNewLine & "(335)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Total deductions under section 16(i), 16(ii) and 16(iii) (specify each deduction separately)" & vbNewLine & "(336)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Income chargeable under the head 'Salaries' (Column 335 minus 336)" & vbNewLine & "(337)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Income (inculding loss from house property) under any head other than income under the head 'salaries' offered for TDS [section 192(2B)]" & vbNewLine & "(338)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Gross total income (Total of columns 337 and 338)" & vbNewLine & "(339)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Amount deductible under section 80G in respect of donations to certain funds, charitable institutions" & vbNewLine & "(340)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Amount deductible under section section 80GG in respect of rents paid" & vbNewLine & "(341)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "Amount deductible under any other provision of chapter VIA (indicate relevant section and amount deucted)" & vbNewLine & "(342)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "Total amount deductible under Chapter VIA (Total of columns 340,341 and 342)" & vbNewLine & "(343)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 19
			.Text = "Total taxable income (Columns 339 minus Columns 343)" & vbNewLine & "(344)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 20
			.Text = "Income-tax on Total income" & vbNewLine & "(345)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 21
			.Text = "Income-tax rebate under section 88 on life insurance premium, contribution to provident fund, etc. [See note 5]" & vbNewLine & "(346)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 22
			.Text = "Income-tax rebate under section 88B" & vbNewLine & "(347)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 23
			.Text = "Income-tax Rebate under section 88C" & vbNewLine & "(348)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 24
			.Text = "Income-tax Rebate under section 88D" & vbNewLine & "(349)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 25
			.Text = "Total Income-tax payable (columns 345 minus total of columns 346, 347, 348 and 349) including surcharge and education cess" & vbNewLine & "(350)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 26
			.Text = "Income-tax relief under section 89, when salary, etc. is paid in arrears or in advance" & vbNewLine & "(351)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 27
			.Text = "Net tax payable (Column 350 minus column 351)" & vbNewLine & "(352)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			'        .Col = 28
			'        .Text = "Tax deducted at source Income-tax" ''''& vbNewLine & "(227)"
			'        .FontBold = True
			'
			'        .Col = 29
			'        .Text = "Surcharge" ''''& vbNewLine & "(228)"
			'        .FontBold = True
			'
			'        .Col = 30
			'        .Text = "Education Cess" ''''& vbNewLine & "(229)"
			'        .FontBold = True
			'
			'        .Col = 31
			'        .Text = "Total income-tax deducted at source (Total of columns 227, 228 and 229)" ''''& vbNewLine & "(230)"
			'        .FontBold = True
			'
			'        .Col = 32
			'        .Text = "Tax payable / refunable (Difference of Columns 226 and 230)" ''''& vbNewLine & "(231)"
			'        .FontBold = True
			'
			'        .Col = 33
			'        .Text = "Remarks (See Notes 6 and 7)" ''''& vbNewLine & "(232)"
			'        .FontBold = True
			
			.Col = 28
			.Text = "Employee Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 29
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	Private Sub FillHeadingSprdViewAnnex1()
		
		With SprdViewAnnex1
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(313)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Employee reference no. provided by employer" & vbNewLine & "(314)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "PAN of the employee" & vbNewLine & "(315)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Name of employee" & vbNewLine & "(316)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Date of payment credit" & vbNewLine & "(317)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Taxable amount on which tax deducted Rs." & vbNewLine & "(318)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "TDS" & vbNewLine & "(319)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Surcharge" & vbNewLine & "(320)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Educ. Cess" & vbNewLine & "(321)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Total Tax deducted (319 + 320 + 321) Rs." & vbNewLine & "(322)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Total Tax depostied Rs." & vbNewLine & "(323)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Date of deduction" & vbNewLine & "(324)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Date of Deposit" & vbNewLine & "(325)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Reason for non-deduction / lowest deduction" & vbNewLine & "(326)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Challan Ref No"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "BSR CODE"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "Deposited Date"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 19
			.Text = "Challan Serial No"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 20
			.Text = "Section Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 21
			.Text = "Total TDS"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 22
			.Text = "Interest"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 23
			.Text = "Others"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 24
			.Text = "Total of the Above"
			.Font = VB6.FontChangeBold(.Font, True)
		End With
	End Sub
	Private Sub frmTDSeReturn24Q_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
		
		txtFYear.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY") & "-" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
		txtAYear.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY") & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")) + 1, "0000")
		txtReturnFiled.Text = "NO"
		txtProvReceiptNo.Text = ""
		
		optAddressChange(1).Checked = True
		optResAddChanged(1).Checked = True
		
		txtPersonName.Text = RsCompany.Fields("COMPANY_NAME").Value
		txtBranch.Text = ""
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
		MainClass.ClearGrid(SprdViewAnnex2, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewChallan, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewAnnex3, RowHeight)
		
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
		
		pFileName = mPubTDSPath & "\eRtn24Q.txt"
		
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
		'    Call PrintDD(mLineCount)
		Call PrintSD(mLineCount)
		'    Call PrintSD16(mLineCount)
		'    Call PrintSD10(mLineCount)
		'    Call PrintSDVIA(mLineCount)
		'    Call PrintSD88(mLineCount)
		
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
	Private Function PrintSD16(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmpSD16Amount As Double) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		
		
		''''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		''''2
		mString = "S16"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''3
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''4
		mString = CStr(pcntRow)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''5
		mString = CStr(1)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''6
		mString = "16(i)"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		
		'''''7
		mString = VB6.Format(mEmpSD16Amount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'''''8
		'    mMainString = mMainString & mDelimited
		
		mLineCount = mLineCount + 1
		PrintLine(1, TAB(0), mMainString)
		
		PrintSD16 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintSD16 = False
		'    Resume
	End Function
	Private Function PrintSD10(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmpSD10Amount As Double) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		
		
		''''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		''''2
		mString = "S10"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''3
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''4
		mString = CStr(pcntRow)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'    ''''5
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''6
		mString = "10OTHERS"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''7
		mString = VB6.Format(mEmpSD10Amount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'''''8
		'    mMainString = mMainString & mDelimited
		
		
		PrintLine(1, TAB(0), mMainString)
		mLineCount = mLineCount + 1
		PrintSD10 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintSD10 = False
		'    Resume
	End Function
	Private Function PrintSDVIA(ByRef pCompany_Code As Integer, ByRef pEmpCode As String, ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmp6AAmount As Double, ByRef mSno As Integer, ByRef m6AType As String) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		Dim mGrossAmount As Double
		Dim mQualifyingAmount As Double
		
		
		If m6AType = "80G" Then
			mGrossAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 48, "AMOUNT1")
			mQualifyingAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 48, "AMOUNT2")
		ElseIf m6AType = "80GG" Then 
			mGrossAmount = 0
			mQualifyingAmount = 0
		Else
			mGrossAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 47, "AMOUNT1")
			mQualifyingAmount = GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 47, "AMOUNT2")
			
			mGrossAmount = mGrossAmount + GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 59, "AMOUNT1")
			mQualifyingAmount = mQualifyingAmount + GetAmountFromDetail(pCompany_Code, RsCompany.Fields("FYEAR").Value, pEmpCode, 59, "AMOUNT2")
		End If
		
		''''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		''''2
		mString = "C6A"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''3
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''4
		mString = CStr(pcntRow)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'    ''''5
		mString = CStr(mSno)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''6
		mString = m6AType
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''7
		mString = VB6.Format(mGrossAmount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'''''8
		mString = VB6.Format(mQualifyingAmount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''9
		mString = VB6.Format(mEmp6AAmount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''10
		'    mMainString = mMainString & mDelimited
		
		
		PrintLine(1, TAB(0), mMainString)
		
		mLineCount = mLineCount + 1
		
		PrintSDVIA = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintSDVIA = False
		'    Resume
	End Function
	Private Function PrintSD88(ByRef mLineCount As Integer, ByRef pcntRow As Integer, ByRef mEmp88Amount As Double, ByRef mSno As Integer, ByRef m88Type As String) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		
		
		''''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		''''2
		mString = "S88"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''3
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''4
		mString = CStr(pcntRow)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'    ''''5
		mString = CStr(mSno)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''6
		mString = m88Type
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''7
		mString = VB6.Format(mEmp88Amount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		'
		'''''8
		mString = VB6.Format(mEmp88Amount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''9
		mString = VB6.Format(mEmp88Amount, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''10
		'    mMainString = mMainString & mDelimited
		
		
		PrintLine(1, TAB(0), mMainString)
		
		mLineCount = mLineCount + 1
		
		PrintSD88 = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintSD88 = False
		'    Resume
	End Function
	
	Private Function PrintFH(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mString As String
		Dim mMainString As String
		
		
		''''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		''''2
		mString = "FH"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''3
		mString = "SL1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''4
		mString = "R"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''5
		mString = VB6.Format(txtRundate.Text, "DDMMYYYY")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''''6
		mString = CStr(mLineCount)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''7
		mString = "D"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''8
		mString = Trim(txtTDSAcNo.Text) & New String(" ", 10 - Len(Trim(txtTDSAcNo.Text)))
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''9
		mString = "1"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''''10
		mMainString = mMainString & mDelimited
		
		'''''11
		mMainString = mMainString & mDelimited
		
		'''''12
		mMainString = mMainString & mDelimited
		
		'''''13
		mMainString = mMainString & mDelimited
		
		'''''14
		mMainString = mMainString & mDelimited
		
		'''''15
		mMainString = mMainString & mDelimited
		
		'''''16
		'    mMainString = mMainString & mDelimited
		
		
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
		
		
		With SprdViewChallan
			For cntRow = 1 To .MaxRows
				
				.Row = cntRow
				.Col = 13
				mCompany_Code = Val(.Text)
				
				.Col = 14
				mMkey = .Text
				
				
				If GetChallan_DedDetail(mDepositAmt, mTDSAmount, mSurchargeAmt, mCESSAmt, mNetAmount, mIntAmt, mOthAmt, mTotDeductee, mCompany_Code, mMkey) = False Then GoTo ErrPart
				
				
				.Row = cntRow
				
				'''1
				.Col = 1
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
				
				'            '''4
				mString = VB6.Format(cntRow, "0")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''5
				mString = VB6.Format(mTotDeductee, "0")
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
				mMainString = mMainString & mDelimited
				
				'''12
				.Col = 11
				mString = VB.Left(Trim(.Text), 5)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''13
				mMainString = mMainString & mDelimited
				
				'''14
				mMainString = mMainString & mDelimited
				
				'''15
				mMainString = mMainString & mDelimited
				
				'''16
				.Col = 9
				mString = VB.Left(Trim(.Text), 7)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''17
				mMainString = mMainString & mDelimited
				
				'''18
				.Col = 10
				mString = VB6.Format(.Text, "DDMMYYYY")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''19
				mMainString = mMainString & mDelimited
				
				'''20
				mMainString = mMainString & mDelimited
				
				'''21
				mString = "92B"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''22 to 27
				For cntCol = 2 To 7
					.Col = cntCol
					mString = VB6.Format(System.Math.Round(Val(.Text), 0), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''28
				mMainString = mMainString & mDelimited
				
				
				'            '''29
				mString = VB6.Format(mDepositAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''30
				mString = VB6.Format(mTDSAmount, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''31
				mString = VB6.Format(mSurchargeAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''32
				mString = VB6.Format(mCESSAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''33
				mNetAmount = mTDSAmount + mSurchargeAmt + mCESSAmt
				mString = VB6.Format(mNetAmount, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''34
				mString = VB6.Format(mIntAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				'
				'            '''35
				mString = VB6.Format(mOthAmt, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''36
				.Col = 8
				mString = VB.Left(Trim(.Text), 15)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''37
				mString = "N"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''38
				mMainString = mMainString & mDelimited
				
				'''39
				'            mMainString = mMainString & mDelimited
				
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
		Dim mRs As Double
		Dim mPaisa As Double
		Dim mCntRow As Integer
		Dim mTotChallanNo As Double
		Dim mTotDeductee As Double
		Dim mChallanAmount As Double
		Dim mDeducteeAmount As Double
		Dim mTotPerquisiteRecd As Double
		Dim mAmountPaid As Double
		Dim mTotalSDRec As Integer
		Dim mGrossTotalIncome As Double
		
		mCntRow = 1
		If GetChallanDetail(mTotChallanNo, mTotDeductee, mTotPerquisiteRecd, mChallanAmount, mDeducteeAmount, mAmountPaid) = False Then GoTo ErrPart
		
		'''1
		mString = CStr(mLineCount)
		mMainString = mString
		mMainString = mMainString & mDelimited
		
		'''2
		mString = "BH"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''3
		mString = CStr(mCntRow)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''4
		mString = VB6.Format(mTotChallanNo, "0")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''5
		mString = "24Q"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''6
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
		mMainString = mMainString & mDelimited
		
		'''12
		mMainString = mMainString & mDelimited
		
		'''13
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mString = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''14
		mMainString = mMainString & mDelimited
		
		'''15
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mString = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'''16
		mString = VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000") & VB6.Format(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, RsCompany.Fields("END_DATE").Value), "YY")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 17
		mString = VB6.Format(Year(RsCompany.Fields("START_DATE").Value), "0000") & VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
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
		
		''' 19
		mString = VB.Left(UCase(Trim(txtPersonName.Text)), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''20
		mString = VB.Left(UCase(Trim(txtBranch.Text)), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 21
		mString = VB.Left(UCase(Trim(txtFlat.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 22
		mString = VB.Left(UCase(Trim(txtBuilding.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 23
		mString = VB.Left(UCase(Trim(txtRoad.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 24
		mString = VB.Left(UCase(Trim(txtArea.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 25
		mString = VB.Left(UCase(Trim(txtTown.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 26
		mString = GetStateCode_TDS((txtState.Text))
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 27
		mString = CStr(Val(VB.Left(txtPinCode.Text, 6)))
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 28
		mString = VB.Left(UCase(Trim(txtEmail.Text)), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 29
		If Trim(txtPhone.Text) = "" Then
			mString = ""
		Else
			mString = Trim(VB.Left(txtPhone.Text, 4))
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 30
		If Trim(txtPhone.Text) = "" Then
			mString = ""
		Else
			mString = Mid(txtPhone.Text, 6, 7)
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 31
		mString = IIf(optAddressChange(0).Checked = True, "Y", "N")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 32
		mString = "O"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 33
		mString = VB.Left(UCase(Trim(txtPersonName_p.Text)), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 34
		mString = VB.Left(UCase(Trim(txtDesg.Text)), 20)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 35
		mString = VB.Left(UCase(Trim(txtFlat_p.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 36
		mString = VB.Left(UCase(Trim(txtBuilding_p.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 37
		mString = VB.Left(UCase(Trim(txtRoad_p.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 38
		mString = VB.Left(UCase(Trim(txtArea_p.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 39
		mString = VB.Left(UCase(Trim(txtTown_p.Text)), 25)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 40
		mString = GetStateCode_TDS((txtState_p.Text))
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 41
		mString = CStr(Val(VB.Left(txtPinCode_p.Text, 6)))
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 42
		mString = VB.Left(UCase(Trim(txtEmail_p.Text)), 75)
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 43
		mString = ""
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 44
		If Trim(txtPhone_p.Text) = "" Then
			mString = ""
		Else
			mString = Trim(VB.Left(txtPhone_p.Text, 4))
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 45
		If Trim(txtPhone_p.Text) = "" Then
			mString = ""
		Else
			mString = Mid(txtPhone_p.Text, 6, 7)
		End If
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 46
		mString = IIf(optResAddChanged(0).Checked = True, "Y", "N")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 47
		mString = VB6.Format(System.Math.Round(mChallanAmount, 0), "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 48
		mString = ""
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		
		Call GetSalaryDetail(mTotalSDRec, mGrossTotalIncome)
		''' 49
		mString = VB6.Format(mTotalSDRec, "0")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		'' 50
		mString = VB6.Format(mGrossTotalIncome, "0.00")
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 51
		mString = "N"
		mMainString = mMainString & mString
		mMainString = mMainString & mDelimited
		
		''' 52
		mMainString = mMainString & mDelimited
		
		''' 53
		'    mMainString = mMainString & mDelimited
		
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
		
		'    pTotChallanNo = SprdViewChallan.MaxRows
		'    pTotDeductee = SprdViewAnnex2.MaxRows
		'    pTotPerquisiteRecd = SprdViewAnnex3.MaxRows
		
		With SprdViewChallan
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 7
				pChallanAmount = pChallanAmount + Val(.Text)
				
				If Val(.Text) > 0 Then
					pTotChallanNo = pTotChallanNo + 1
				End If
			Next 
		End With
		
		With SprdViewAnnex1
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 6
				pAmountPaid = pAmountPaid + Val(.Text)
				
				.Col = 11
				pDeducteeAmount = pDeducteeAmount + Val(.Text)
				
				.Col = 2
				If Trim(.Text) <> "" Then
					pTotDeductee = pTotDeductee + 1
				End If
			Next 
		End With
		
		'    With SprdViewAnnex2
		'        For cntRow = 1 To .MaxRows
		'            .Row = cntRow
		'            .Col = 28
		'            If Trim(.Text) <> "" Then
		'                pTotDeductee = pTotDeductee + 1
		'            End If
		'        Next
		'    End With
		
		With SprdViewAnnex3
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 18
				If Trim(.Text) <> "" Then
					pTotPerquisiteRecd = pTotPerquisiteRecd + 1
				End If
			Next 
		End With
		
		GetChallanDetail = True
		Exit Function
ErrPart1: 
		GetChallanDetail = False
	End Function
	Private Function PrintDD(ByRef mLineCount As Integer, ByRef pCompany_Code As Integer, ByRef pMkey As String, ByRef pChallanLineNo As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		Dim i As Integer
		
		mString = ""
		With SprdViewAnnex1
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 16
				If Trim(pMkey) = Trim(.Text) Then
					'''1
					mString = CStr(mLineCount)
					mMainString = mString
					mMainString = mMainString & mDelimited
					
					'''2
					mString = "DD"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''''3
					mString = "1"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'            '''4
					mString = CStr(pChallanLineNo)
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'            '''5
					.Col = 1
					mString = CStr(Val(.Text))
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					''6
					mString = "O"
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					''7
					.Col = 2
					mString = VB.Left(UCase(Trim(.Text)), 9)
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''8
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
					.Col = 4
					mString = VB.Left(UCase(Trim(.Text)), 75)
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''14 to 17
					For i = 7 To 10
						.Col = i
						mString = VB6.Format(.Text, "0.00")
						mMainString = mMainString & mString
						mMainString = mMainString & mDelimited
					Next 
					
					'''18
					mMainString = mMainString & mDelimited
					
					'''19
					.Col = 11
					mString = VB6.Format(.Text, "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''20
					mMainString = mMainString & mDelimited
					
					'''21
					mMainString = mMainString & mDelimited
					
					'''22
					.Col = 6
					mString = VB6.Format(.Text, "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''23
					.Col = 5
					mString = VB6.Format(.Text, "DDMMYYYY")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''24
					.Col = 12
					mString = VB6.Format(.Text, "DDMMYYYY")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''25
					.Col = 13
					mString = VB6.Format(.Text, "DDMMYYYY")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
					
					'''26 to 33
					For i = 26 To 32
						mMainString = mMainString & mDelimited
					Next 
					
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
		
		SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(ID.AMOUNT) AS DEPOSIT_AMOUNT, " & vbCrLf & " SUM(ID.TDS_AMOUNT) AS TOTTDSAMOUNT, " & vbCrLf & " SUM(ID.SURCHARGE_AMT) AS TOTSURCHARGE, " & vbCrLf & " SUM(ID.CESS_AMT) AS TOTEDU_CESS, " & vbCrLf & " SUM(ID.AMOUNT) AS TOTNET_AMOUNT, " & vbCrLf & " 0 AS TOTINTEREST_AMOUNT, " & vbCrLf & " 0 AS TOTOTHER_AMOUNT "
		
		SqlStr = SqlStr & vbCrLf & " FROM PAY_ITCHALLAN_HDR IH, PAY_ITCHALLAN_DET ID "
		
		SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.AUTO_KEY_REFNO=" & Val(pMkey) & " " & vbCrLf & " AND IH.VDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND IH.VDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & pCompany_Code & ""
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf
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
	Private Function PrintSD(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mEmpCode As String
		Dim mEmpSD16Amount As Double
		Dim mEmpSD10Amount As Double
		Dim mEmp80GAmount As Double
		Dim mEmp80GGAmount As Double
		Dim mEmp80OthersAmount As Double
		Dim mEmp88Amount As Double
		Dim mEmp88BAmount As Double
		Dim mEmp88CAmount As Double
		Dim mEmp88DAmount As Double
		
		
		Dim mEmpSD16Count As Integer
		Dim mEmpSD10Count As Integer
		Dim mEmp6ACount As Integer
		Dim mEmp88Count As Integer
		Dim mSNO6A As Integer
		Dim mSNO88 As Integer
		Dim mCompanyCode As Integer
		
		Dim i As Integer
		
		With SprdViewAnnex2
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				
				.Col = 11
				mEmpSD16Amount = Val(.Text)
				
				.Col = 9
				mEmpSD10Amount = Val(.Text)
				
				.Col = 15
				mEmp80GAmount = Val(.Text)
				
				.Col = 16
				mEmp80GGAmount = Val(.Text)
				
				.Col = 17
				mEmp80OthersAmount = Val(.Text)
				
				.Col = 21
				mEmp88Amount = Val(.Text)
				
				.Col = 22
				mEmp88BAmount = Val(.Text)
				
				.Col = 23
				mEmp88CAmount = Val(.Text)
				
				.Col = 24
				mEmp88DAmount = Val(.Text)
				
				mEmpSD16Count = IIf(mEmpSD16Amount > 0, 1, 0)
				mEmpSD10Count = IIf(mEmpSD10Amount > 0, 1, 0)
				mEmp6ACount = IIf(mEmp80GAmount > 0, 1, 0)
				mEmp6ACount = mEmp6ACount + IIf(mEmp80GGAmount > 0, 1, 0)
				mEmp6ACount = mEmp6ACount + IIf(mEmp80OthersAmount > 0, 1, 0)
				
				mEmp88Count = IIf(mEmp88Amount > 0, 1, 0)
				mEmp88Count = mEmp88Count + IIf(mEmp88BAmount > 0, 1, 0)
				mEmp88Count = mEmp88Count + IIf(mEmp88CAmount > 0, 1, 0)
				mEmp88Count = mEmp88Count + IIf(mEmp88DAmount > 0, 1, 0)
				
				'''1
				mString = CStr(mLineCount)
				mMainString = mString
				mMainString = mMainString & mDelimited
				
				'''2
				mString = "SD"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''''3
				mString = CStr(1)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''4
				.Col = 1
				mString = CStr(Val(CStr(cntRow)))
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				''5
				mString = "A"
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				
				'''6
				mMainString = mMainString & mDelimited
				
				'''7
				.Col = 2
				If Len(Trim(.Text)) = 10 Then
					mString = UCase(Trim(.Text))
				Else
					mString = "PANINVALID"
				End If
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''8
				mMainString = mMainString & mDelimited
				
				'''9
				.Col = 3
				'            mEmpCode = Trim(.Text)
				mString = VB.Left(UCase(Trim(.Text)), 75)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''10
				'            .Col = 3
				.Col = 28
				mEmpCode = Trim(.Text)
				
				.Col = 29
				mCompanyCode = CInt(Trim(.Text))
				
				mString = GetEmpDesg(mEmpCode, mCompanyCode)
				mString = VB.Left(mString, 15)
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''11
				.Col = 4
				mString = VB6.Format(.Text, "DDMMYYYY")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''12
				.Col = 5
				mString = VB6.Format(.Text, "DDMMYYYY")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''13 to 15
				For i = 6 To 8
					.Col = i
					mString = VB6.Format(Val(.Text), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''16
				mMainString = mMainString & mDelimited
				
				'''17
				mString = VB6.Format(mEmpSD10Count, "0")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''18 to 19
				For i = 9 To 10
					.Col = i
					mString = VB6.Format(Val(.Text), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''20
				mString = VB6.Format(mEmpSD16Count, "0")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''21 to 24
				For i = 11 To 14
					.Col = i
					mString = VB6.Format(Val(.Text), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''25
				mMainString = mMainString & mDelimited
				
				'''26
				mString = VB6.Format(mEmp6ACount, "0")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				
				'''27 to 29
				For i = 18 To 20
					.Col = i
					mString = VB6.Format(Val(.Text), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''30
				mString = VB6.Format(mEmp88Count, "0")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''31
				.Col = 21
				mString = CStr(Val(.Text))
				.Col = 22
				mString = CStr(CDbl(mString) + Val(.Text))
				.Col = 23
				mString = CStr(CDbl(mString) + Val(.Text))
				.Col = 24
				mString = CStr(CDbl(mString) + Val(.Text))
				mString = VB6.Format(mString, "0.00")
				mMainString = mMainString & mString
				mMainString = mMainString & mDelimited
				
				'''32 to 34
				For i = 25 To 27
					.Col = i
					mString = VB6.Format(Val(.Text), "0.00")
					mMainString = mMainString & mString
					mMainString = mMainString & mDelimited
				Next 
				
				'''35 to 47
				With SprdViewAnnex3
					.Row = 1
					For i = 3 To 15
						.Col = i
						mString = VB6.Format(Val(.Text), "0.00")
						mMainString = mMainString & mString
						mMainString = mMainString & mDelimited
					Next 
				End With
				
				''48 to 52
				
				For i = 48 To 51
					mMainString = mMainString & mDelimited
				Next 
				
				PrintLine(1, TAB(0), mMainString)
				
				mLineCount = mLineCount + 1
				
				If mEmpSD16Count > 0 Then
					Call PrintSD16(mLineCount, cntRow, mEmpSD16Amount)
				End If
				
				If mEmpSD10Count > 0 Then
					Call PrintSD10(mLineCount, cntRow, mEmpSD10Amount)
				End If
				
				mSNO6A = 1
				mSNO88 = 1
				If mEmp80GAmount > 0 Then
					Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80GAmount, mSNO6A, "80G")
					mSNO6A = mSNO6A + 1
				End If
				
				If mEmp80GGAmount > 0 Then
					Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80GGAmount, mSNO6A, "80GG")
					mSNO6A = mSNO6A + 1
				End If
				
				If mEmp80OthersAmount > 0 Then
					Call PrintSDVIA(mCompanyCode, mEmpCode, mLineCount, cntRow, mEmp80OthersAmount, mSNO6A, "80OTHERS")
				End If
				
				If mEmp88Amount > 0 Then
					Call PrintSD88(mLineCount, cntRow, mEmp88Amount, mSNO88, "88")
					mSNO88 = mSNO88 + 1
				End If
				
				If mEmp88BAmount > 0 Then
					Call PrintSD88(mLineCount, cntRow, mEmp88BAmount, mSNO88, "88B")
					mSNO88 = mSNO88 + 1
				End If
				
				If mEmp88CAmount > 0 Then
					Call PrintSD88(mLineCount, cntRow, mEmp88CAmount, mSNO88, "88C")
					mSNO88 = mSNO88 + 1
				End If
				
				If mEmp88DAmount > 0 Then
					Call PrintSD88(mLineCount, cntRow, mEmp88CAmount, mSNO88, "88D")
				End If
				
				mEmpSD16Amount = 0
				mEmpSD10Amount = 0
				mEmp80GAmount = 0
				mEmp80GGAmount = 0
				mEmp80OthersAmount = 0
				mEmp88Amount = 0
				mEmp88BAmount = 0
				mEmp88CAmount = 0
				mEmp88DAmount = 0
				
				mEmpSD16Count = 0
				mEmpSD10Count = 0
				mEmp6ACount = 0
				mEmp88Count = 0
				
			Next 
		End With
		PrintSD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		PrintSD = False
		'    Resume
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetEmpDesg(ByRef pEmpCode As String, ByRef pCompanyCode As Integer) As String
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim RsDesig As ADODB.Recordset
		Dim SqlStr As String
		
		SqlStr = " SELECT EMP_DESG_CODE from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf & " AND EMP_CODE='" & pEmpCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')) "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesig, ADODB.LockTypeEnum.adLockOptimistic)
		
		If RsDesig.EOF = False Then
			If RsDesig.Fields("EMP_DESG_CODE").Value <> "" Then
				'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ValidateWithMasterTable. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If MainClass.ValidateWithMasterTable(RsDesig.Fields("EMP_DESG_CODE"), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & pCompanyCode & "") = True Then
					'UPGRADE_WARNING: Couldn't resolve default property of object MasterNo. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					GetEmpDesg = MasterNo
				End If
			End If
		End If
		Exit Function
ERR1: 
		MsgInformation(Err.Description)
	End Function
	
	Private Function GetSalaryDetail(ByRef pTotalSDRec As Integer, ByRef mGrossTotalIncome As Double) As Boolean
		On Error GoTo ErrPart
		Dim cntRow As Integer
		
		mGrossTotalIncome = 0
		With SprdViewAnnex2
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = 14
				mGrossTotalIncome = mGrossTotalIncome + Val(.Text)
				
				If Val(.Text) <> 0 Then
					pTotalSDRec = pTotalSDRec + 1
				End If
			Next 
		End With
		'    mGrossTotalIncome = Round(mGrossTotalIncome, 0)
		GetSalaryDetail = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		GetSalaryDetail = False
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