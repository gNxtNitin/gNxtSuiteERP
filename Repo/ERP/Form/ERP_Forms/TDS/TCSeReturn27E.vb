Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmTCSeReturn27E
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
		Call CreateCD("V")
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
		Me.Close()
	End Sub
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForTCS(Crystal.DestinationConstants.crptToWindow)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ReportForTCS(ByRef Mode As Crystal.DestinationConstants)
		Dim MainClass_Renamed As Object
		
		On Error GoTo ERR1
		Dim All As Boolean
		Dim SqlStr As String
		Dim mTitle As String
		Dim mSubTitle As String
		Dim PrintStatus As Boolean
		Dim mReportFileName As String
		Dim mSection As String
		Dim mDetail As String
		Dim mFormTitle As String
		PubDBCn.Errors.Clear()
		
		PrintStatus = False
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearCRptFormulas(Report1)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = "DELETE FROM TEMP_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		PubDBCn.Execute(SqlStr)
		
		SqlStr = ""
		
		'''''Select Record for print...
		frmPrintTDS.OptForm26.Text = "Form 27E"
		'    frmPrintTDS.OptForm27A.Enabled = False
		frmPrintTDS.OptAnnexure2.Enabled = False
		frmPrintTDS.OptAnnexure3.Enabled = False
		frmPrintTDS.fraAnnx.Enabled = False
		
		frmPrintTDS.ShowDialog()
		
		If G_PrintLedg = False Then
			Exit Sub
		End If
		
		Call InsertIntoPrintDummy()
		
		If frmPrintTDS.OptForm26.Checked = True Then
			mTitle = "Form No. 27E"
			mSubTitle = "[See section 206C and rule 37E]"
			mFormTitle = "Annual return of collection of tax under section 206C of Income-tax Act, 1961 in respect of collections for the period ending 31st March, " & Year(RsCompany.Fields("END_DATE").Value)
			
			mReportFileName = "TDSeReturn27E.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, mFormTitle)
		ElseIf frmPrintTDS.OptForm27A.Checked = True Then 
			
			mTitle = "Form No. 27A"
			mSubTitle = "[See rule 37B"
			
			mReportFileName = "TDSeReturn27A.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, "27E")
		Else
			mTitle = "A N N E X U R E"
			mSubTitle = "Detail of purchase price of [E] Scrap debited / received for the period ending 31st March, " & Year(RsCompany.Fields("END_DATE").Value) & " and of tax collected at source"
			mFormTitle = ""
			mReportFileName = "TDSeReturn27EAnnx.rpt"
			SqlStr = ""
			SqlStr = FetchRecordForReport(SqlStr)
			Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle, "")
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
			If InsertGridDetail(SprdViewChallan, 1, (SprdViewChallan.MaxRows), (SprdViewChallan.MaxCols)) = False Then GoTo ERR1
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
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mFormTitle As String)
		Dim MainClass_Renamed As Object
		
		Dim mTotAmountPaid As Double
		Dim mTotDeduct As Double
		Dim mTotPerson As Double
		Dim mTotAnnexNo As Double
		Dim mChallanAmount As Double
		
		Dim mPartyName As String
		Dim mAYEAR As String
		Dim cntRow As Integer
		Dim mTANNo As String
		Dim mPANNo As String
		Dim mFYear As String
		
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "FormTitle=""" & mFormTitle & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AuthName=""" & Trim(txtPersonName.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Designation=""" & Trim(txtDesg.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "Date=""" & Trim(txtRundate.Text) & """")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "CompanyName=""" & Trim(txtCompanyName.Text) & """")
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
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mTANNo = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		mPANNo = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "TANNo=""" & Trim(mTANNo) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "PANNo=""" & Trim(mPANNo) & """")
		
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "AddresChange=""" & Trim(txtAddressChange.Text) & """")
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignCRptFormulas(Report1, "DeductorStatus=""" & Trim(txtDeductorStatus.Text) & """")
		
		If frmPrintTDS.OptForm27A.Checked = True Then
			
			
			With SprdViewAnnex
				For cntRow = 1 To .MaxRows
					.Row = cntRow
					.Col = 12
					mTotAmountPaid = mTotAmountPaid + Val(.Text)
					
					.Col = 15
					mTotDeduct = mTotDeduct + Val(.Text)
					
					.Col = 5
					'                If mPartyName <> Trim(.Text) Then
					mTotPerson = mTotPerson + 1
					'                End If
					'                mPartyName = Trim(.Text)
				Next 
			End With
			
			With SprdViewChallan
				For cntRow = 1 To .MaxRows
					.Row = cntRow
					
					.Col = 2
					If mPartyName <> Trim(.Text) Then
						mTotAnnexNo = mTotAnnexNo + 1
					End If
					mPartyName = Trim(.Text)
					
					.Col = 8
					mChallanAmount = mChallanAmount + Val(.Text)
					
				Next 
				
			End With
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAmountPaid=""" & VB6.Format(mTotAmountPaid, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotDeduct=""" & VB6.Format(mTotDeduct, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotPerson=""" & mTotPerson & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotChallanAmount=""" & VB6.Format(mChallanAmount, "0.00") & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "TotAnnexNo=""" & mTotAnnexNo & """")
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "FormName=""" & mFormTitle & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "PeriodEnding=""" & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & """")
			mAYEAR = VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000") & "-" & VB6.Format(Year(RsCompany.Fields("END_DATE").Value) + 1, "0000")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "AYEAR=""" & mAYEAR & """")
			
			'    MainClass.AssignCRptFormulas Report1, "AuthName=""" & Trim(txtPersonName.Text) & """"
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "DeductorType=""OTHERS""")
			mFYear = VB6.Format(Year(RsCompany.Fields("START_DATE").Value), "0000") & "-" & VB6.Format(Year(RsCompany.Fields("END_DATE").Value), "0000")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "FYEAR=""" & mFYear & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "PersonName=""" & txtPersonName.Text & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "Flat_P=""" & txtFlat.Text & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "Building_P=""" & Trim(txtBuilding.Text) & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "Road_P=""" & Trim(txtRoad.Text) & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "Area_P=""" & Trim(txtArea.Text) & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "Town_P=""" & txtTown.Text & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "State_P=""" & Trim(txtState.Text) & """")
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignCRptFormulas. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.AssignCRptFormulas(Report1, "PinCode_P=""" & Trim(txtPinCode.Text) & """")
			
		End If
		
		
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
		mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
		
		mSqlStr = mSqlStr & " ORDER BY SUBROW"
		FetchRecordForReport = mSqlStr
		
	End Function
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForTCS(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	Private Sub cmdValidate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdValidate.Click
		Call Shell(My.Application.Info.DirectoryPath & "\NeweReturn.exe", AppWinStyle.NormalFocus)
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
		
		FieldsVerification = True
		Exit Function
ERR1: 
		FieldsVerification = False
	End Function
	'UPGRADE_WARNING: Form event frmTCSeReturn27E.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Public Sub frmTCSeReturn27E_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
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
	Private Sub frmTCSeReturn27E_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		On Error GoTo BSLError
		Dim mFDate As String
		
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
		mFDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 7, CDate(VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY"))))
		txtFDate.Text = VB6.Format(mFDate, "DD/MM/YYYY")
		
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
		
		If ShowDetailChallan = False Then GoTo ErrPart
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
		Dim mAddress1 As String
		Dim mAddress2 As String
		Dim mAddress3 As String
		Dim mAddress4 As String
		Dim mBuyerCode As String
		Dim mAmountPurchase As Double
		
		SqlStr = " SELECT " & vbCrLf & " TRN.COLLECTIONCODE,  CMST.PAN_NO, " & vbCrLf & " CMST.SUPP_CUST_NAME, " & vbCrLf & " CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, " & vbCrLf & " CMST.SUPP_CUST_STATE, CMST.SUPP_CUST_PIN, CMST.CType," & vbCrLf & " IH.NETVALUE, IH.NETTAXAMOUNT, IH.INVOICE_DATE, " & vbCrLf & " IH.TCSPER, IH.TCSAMOUNT, IH.INVOICE_DATE, " & vbCrLf & " TRN.BANKCODE, TRN.CHALLANDATE, TRN.CHALLANNO, " & vbCrLf & " '' AS CDate, IH.COMPANY_CODE "
		
		
		SqlStr = SqlStr & vbCrLf & " FROM TCS_TRN IH, TCS_CHALLAN TRN,FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE "
		
		
		SqlStr = SqlStr & vbCrLf & " IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=TRN.FYEAR" & vbCrLf & " AND IH.TCSCHALLANMKEY =TRN.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.ISTCSPAID= 'Y'" & vbCrLf & " AND IH.CANCELLED='N'" & vbCrLf & " AND TRN.REFDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.REFDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY  COLLECTIONCODE, TRN.COMPANY_CODE, CMST.SUPP_CUST_NAME, TRN.REFDATE"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdViewAnnex
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.Row = cntRow
					.Col = 1
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mBuyerCode = IIf(IsDbNull(RsTemp.Fields("CType").Value), "N", RsTemp.Fields("CType").Value)
					.Text = IIf(mBuyerCode = "C", "01", "02")
					
					'                .Text = IIf(IsNull(RsTemp!COLLECTIONCODE), "", RsTemp!COLLECTIONCODE)
					
					.Col = 2
					If Len(RsTemp.Fields("PAN_NO").Value) <> 10 Then
						.Text = ""
					Else
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						.Text = IIf(IsDbNull(RsTemp.Fields("PAN_NO").Value), "", RsTemp.Fields("PAN_NO").Value)
					End If
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mAddress = Trim(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
					mAddress = Trim(Trim(Replace(mAddress, vbNewLine, ", ")))
					
					'                mAddress = GetMultiLine(mAddress, 1, 25, 1)
					mAddress1 = Trim(VB.Left(mAddress, InStr(1, mAddress, ",")))
					mAddress = Trim(Mid(mAddress, Len(mAddress1) + 1))
					
					mAddress2 = Trim(VB.Left(mAddress, InStr(1, mAddress, ",")))
					mAddress = Trim(Mid(mAddress, Len(mAddress2) + 1))
					
					mAddress3 = Trim(VB.Left(mAddress, InStr(1, mAddress, ",")))
					mAddress = Trim(Mid(mAddress, Len(mAddress3) + 1))
					
					mAddress4 = Trim(VB.Left(mAddress, InStr(1, mAddress, ",")))
					mAddress = Trim(Mid(mAddress, Len(mAddress4) + 1))
					
					
					.Col = 4
					.Text = VB.Left(mAddress1, 25)
					
					.Col = 5
					.Text = VB.Left(mAddress2, 25)
					
					.Col = 6
					.Text = VB.Left(mAddress3, 25)
					
					.Col = 7
					.Text = VB.Left(mAddress4, 25)
					
					.Col = 8
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
					
					.Col = 9
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = GetStateCode_TDS(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value))
					
					.Col = 10
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB.Left(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value), 6)
					
					.Col = 11
					
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mAmountPurchase = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mAmountPurchase = mAmountPurchase - IIf(IsDbNull(RsTemp.Fields("NETTAXAMOUNT").Value), 0, RsTemp.Fields("NETTAXAMOUNT").Value)
					
					.Text = VB6.Format(mAmountPurchase, "0.00") ' Format(IIf(IsNull(RsTemp!NETVALUE), "", RsTemp!NETVALUE), "0.00")
					
					.Col = 12
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), "", RsTemp.Fields("NETVALUE").Value), "0.00")
					
					.Col = 13
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
					
					.Col = 14
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TCSPER").Value), "", RsTemp.Fields("TCSPER").Value), "0.00")
					
					.Col = 15
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("NETTAXAMOUNT").Value), "", RsTemp.Fields("NETTAXAMOUNT").Value), "0.00") '' Format(IIf(IsNull(RsTemp!TCSAMOUNT), "", RsTemp!TCSAMOUNT), "0.00")
					
					.Col = 16
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
					
					.Col = 17
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("BANKCODE").Value), "", RsTemp.Fields("BANKCODE").Value)
					
					.Col = 18
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("CHALLANDATE").Value), "", RsTemp.Fields("CHALLANDATE").Value), "DD/MM/YYYY")
					
					.Col = 19
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("CHALLANNO").Value), "", RsTemp.Fields("CHALLANNO").Value)
					
					.Col = 20
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(txtFDate.Text), "", txtFDate.Text), "DD/MM/YYYY")
					
					.Col = 21
					.Text = "X" ' DD "A"
					
					.Col = 22
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
		
		SqlStr = "Select COLLECTIONCODE,  TCS_AMOUNT, SURCHARGE, EDU_CESS, INTEREST_AMOUNT," & vbCrLf & " OTHER_AMOUNT, PAIDAMOUNT, " & vbCrLf & " BANKCODE, CHQ_NO, CHQ_DATE, " & vbCrLf & " CHALLANNO , CHALLANDATE, COMPANY_CODE "
		
		SqlStr = SqlStr & vbCrLf & " FROM TCS_CHALLAN TRN " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " REFDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND REFDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
		If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.COMPANY_CODE, CHALLANDATE, CHALLANNO "
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		cntRow = 1
		
		With SprdViewChallan
			If RsTemp.EOF = False Then
				Do While Not RsTemp.EOF
					.Row = cntRow
					.Col = 1
					.Text = CStr(cntRow)
					
					.Col = 2
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = IIf(IsDbNull(RsTemp.Fields("COLLECTIONCODE").Value), "", RsTemp.Fields("COLLECTIONCODE").Value)
					
					.Col = 3
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TCS_AMOUNT").Value), "", RsTemp.Fields("TCS_AMOUNT").Value), "0.00")
					
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
					.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PAIDAMOUNT").Value), "", RsTemp.Fields("PAIDAMOUNT").Value), "0.00")
					
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
					.Text = "N"
					
					RsTemp.MoveNext()
					If RsTemp.EOF = False Then
						cntRow = cntRow + 1
						.MaxRows = cntRow
					End If
				Loop 
			End If
		End With
		ShowDetailChallan = True
		Exit Function
ErrPart1: 
		ShowDetailChallan = False
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
		Call FormatSprdViewAnnex()
	End Sub
	
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewChallan()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewChallan
			.MaxCols = 14
			
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
			
			FillHeadingSprdViewChallan()
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdViewChallan, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdViewChallan, 1, .MaxRows, 1, .MaxCols)
			.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
		End With
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdViewAnnex()
		Dim MainClass_Renamed As Object
		Dim i As Integer
		With SprdViewAnnex
			.MaxCols = 22
			
			.set_RowHeight(0, RowHeight * 3.5)
			
			.set_ColWidth(0, 0)
			
			.set_RowHeight(-1, RowHeight)
			.Row = -1
			
			.Col = 1
			.CellType = SS_CELL_TYPE_STATIC_TEXT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_ColWidth(.Col, 6)
			
			.Col = 2
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 8)
			
			.Col = 3
			.CellType = SS_CELL_TYPE_EDIT
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.TypeMaxEditLen = 255
			.set_ColWidth(.Col, 35)
			
			For i = 4 To 10
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 10)
				.ColHidden = True
			Next 
			
			For i = 11 To 12
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
			
			For i = 13 To 13
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 12)
			Next 
			
			For i = 14 To 15
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
			
			For i = 16 To 22
				.Col = i
				.CellType = SS_CELL_TYPE_EDIT
				.TypeHAlign = SS_CELL_H_ALIGN_LEFT
				.TypeMaxEditLen = 255
				.set_ColWidth(i, 12)
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
			.Text = "Buyer Code" & vbNewLine & "(614)" & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "PAN of the person from whow tax collection" & vbNewLine & "(615)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "Name of the person from whow tax collected (Address need not be given if PAN is mentioned)" & vbNewLine & "(616)" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Addres1"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Address2"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Address3"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Address4"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Address5"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "State"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "Pin Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Total Value of the Purchase(s)" & vbNewLine & "(617)" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Amount of payment (Rs.)" & vbNewLine & "(618)" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Date on Which amount paid / debited" & vbNewLine & "(619)" & vbNewLine & "(6)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Rate at which tax collected" & vbNewLine & "(620)" & vbNewLine & "(7)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 15
			.Text = "Amount of tax collected (Rs.)" & vbNewLine & "(621)" & vbNewLine & "(8)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 16
			.Text = "Date on which tax collected" & vbNewLine & "(622)" & vbNewLine & "(9)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 17
			.Text = "Bank-Branch Code" & vbNewLine & "(623)" & vbNewLine & "(10)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 18
			.Text = "Date on which tax paid to the credit of Central Govt" & vbNewLine & "(624)" & vbNewLine & "(11)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 19
			.Text = "Challan No Given By Bank / Tr. Voucher No." & vbNewLine & "(625)" & vbNewLine & "(12)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 20
			.Text = "Date of Furnishing Tax collection Certificate" & vbNewLine & "(626)" & vbNewLine & "(13)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 21
			.Text = "Reason for Non-Deduction / Lower Deduction, if any" & vbNewLine & "(627)" & vbNewLine & "(14)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 22
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
		End With
	End Sub
	
	Private Sub FillHeadingSprdViewChallan()
		
		With SprdViewChallan
			.Row = 0
			
			.Col = 1
			.Text = "S.No." & vbNewLine & "(601)" & vbNewLine & "(1)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 2
			.Text = "Collection Code" & vbNewLine & "(602)" & vbNewLine & "(2)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 3
			.Text = "TCS Rs." & vbNewLine & "(603)" & vbNewLine & "(3)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 4
			.Text = "Surcharge Rs." & vbNewLine & "(604)" & vbNewLine & "(4)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 5
			.Text = "Education Cess Rs." & vbNewLine & "(605)" & vbNewLine & "(5)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 6
			.Text = "Interest Rs." & vbNewLine & "(606)" & vbNewLine & "(6)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 7
			.Text = "Others Rs." & vbNewLine & "(607)" & vbNewLine & "(7)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 8
			.Text = "Total Tax deposited Rs." & vbNewLine & "(608)" & vbNewLine & "(8)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 9
			.Text = "Cheque/DD No." & vbNewLine & "(609)" & vbNewLine & "(9)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 10
			.Text = "BSR Code" & vbNewLine & "(610)" & vbNewLine & "(10)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 11
			.Text = "Date on which tax deposted" & vbNewLine & "(611)" & vbNewLine & "(11)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 12
			.Text = "Transfer Voucher/Challan Serial Number" & vbNewLine & "(612)" & vbNewLine & "(12)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 13
			.Text = "Whether TDS deposited by book entry (Y/N/)" & vbNewLine & "(613)" & vbNewLine & "(13)"
			.Font = VB6.FontChangeBold(.Font, True)
			
			.Col = 14
			.Text = "Company Code"
			.Font = VB6.FontChangeBold(.Font, True)
			
			
		End With
	End Sub
	Private Sub frmTCSeReturn27E_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		FormActive = False
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		Dim mMonthType As String
		
		txtRundate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
		txtAddressChange.Text = "N"
		
		txtReturnPeriod.Text = "Y"
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPersonName.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtDesg.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
		
		txtCompanyName.Text = RsCompany.Fields("COMPANY_NAME").Value
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
		txtTDSAcNo.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPanNo.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
		txtTDSAcNo.Enabled = False
		txtPanNo.Enabled = False
		txtDeductorStatus.Text = "O"
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewChallan, RowHeight)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdViewAnnex, RowHeight)
		
		
	End Sub
	Private Sub SetTextLength()
		On Error GoTo ERR1
		'UPGRADE_WARNING: TextBox property txtRundate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtRundate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property txtAddressChange.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtAddressChange.Maxlength = 1
		'UPGRADE_WARNING: TextBox property txtDeductorStatus.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDeductorStatus.Maxlength = 1
		'UPGRADE_WARNING: TextBox property txtReturnPeriod.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtReturnPeriod.Maxlength = 1
		'UPGRADE_WARNING: TextBox property txtPersonName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPersonName.Maxlength = 75
		'UPGRADE_WARNING: TextBox property txtDesg.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDesg.Maxlength = 20
		'UPGRADE_WARNING: TextBox property txtCompanyName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtCompanyName.Maxlength = 75
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
		Exit Sub
ERR1: 
		MsgBox(Err.Description)
	End Sub
	Private Function CreateCD(ByRef pPrintMode As String) As Boolean
		On Error GoTo ErrPart
		Dim pFileName As String
		Dim mLineCount As Integer
		Dim FilePath As String
		
		pFileName = mPubTDSPath & "\eReturn27E.txt"
		
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
		Call PrintDD(mLineCount)
		
		FileClose(1)
		
		Shell("ATTRIB +R -A " & pFileName)
		Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
		
		CreateCD = True
		Exit Function
ErrPart: 
		MsgBox(Err.Description)
		CreateCD = False
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
		
		mString = "NS3"
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
				mString = "206C"
				mString = Trim(mString) & New String(" ", 5 - Len(mString))
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
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''12
				.Col = 9
				mString = CStr(Val(VB.Left(Trim(.Text), 14)))
				mString = VB6.Format(mString, "00000000000000")
				mMainString = mMainString & mString
				
				'''13
				.Col = 10
				mString = VB.Left(Trim(.Text), 7)
				mString = mString & New String(" ", 7 - Len(mString))
				mMainString = mMainString & mString
				
				'''14
				.Col = 11
				mString = VB6.Format(Trim(.Text), "DDMMYYYY")
				mMainString = mMainString & mString
				
				'''15
				.Col = 12
				mString = VB.Left(Trim(.Text), 9)
				mString = mString & New String(" ", 9 - Len(mString))
				mMainString = mMainString & mString
				
				''16
				.Col = 13
				mString = VB.Left(Trim(.Text), 1)
				mMainString = mMainString & mString
				
				'''17
				'            mString = String(1, " ")
				.Col = 2
				mString = Trim(.Text)
				mString = Trim(.Text) & New String(" ", 1 - Len(mString))
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
		
		mCntRow = 1
		If GetChallanDetail(mTotChallanNo, mTotDeductee, mChallanAmount, mDeducteeAmount) = False Then GoTo ErrPart
		
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
		
		'''6
		mString = "27E" & New String(" ", 4 - Len("27E"))
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
		mString = UCase(RsCompany.Fields("COMPANY_NAME").Value)
		mString = mString & New String(" ", 75 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		
		''' 13
		mString = VB.Left(UCase(Trim(txtFlat.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 14
		mString = VB.Left(UCase(Trim(txtBuilding.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 15
		mString = VB.Left(UCase(Trim(txtRoad.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 16
		mString = VB.Left(UCase(Trim(txtArea.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 17
		mString = VB.Left(UCase(Trim(txtTown.Text)), 25)
		mString = mString & New String(" ", 25 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		
		''' 18
		mString = GetStateCode_TDS((txtState.Text))
		mString = VB6.Format(mString, "00")
		mMainString = mMainString & mString
		
		''' 19
		mString = VB.Left(UCase(Trim(txtPinCode.Text)), 6)
		mString = VB6.Format(mString, "000000")
		mMainString = mMainString & mString
		
		''' 20
		mString = VB.Left(UCase(Trim(txtAddressChange.Text)), 1)
		mString = mString & New String(" ", 1 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 21
		mString = VB.Left(UCase(Trim(txtDeductorStatus.Text)), 1)
		mString = mString & New String(" ", 1 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 22
		mString = "Y"
		mString = mString & New String(" ", 2 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 23
		mString = VB.Left(UCase(Trim(txtPersonName.Text)), 75)
		mString = mString & New String(" ", 75 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 24
		mString = VB.Left(UCase(Trim(txtDesg.Text)), 20)
		mString = mString & New String(" ", 20 - Len(Trim(mString)))
		mMainString = mMainString & mString
		
		''' 25
		mString = VB6.Format(mChallanAmount, "0.00")
		mRs = CDbl(Mid(Trim(mString), 1, InStr(1, Trim(mString), ".") - 1))
		mPaisa = CDbl(Mid(Trim(mString), InStr(1, Trim(mString), ".") + 1))
		mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
		mMainString = mMainString & mString
		
		''' 26
		mString = VB6.Format(mDeducteeAmount, "0.00")
		mRs = CDbl(Mid(Trim(mString), 1, InStr(1, Trim(mString), ".") - 1))
		mPaisa = CDbl(Mid(Trim(mString), InStr(1, Trim(mString), ".") + 1))
		mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
		mMainString = mMainString & mString
		
		'''27
		mString = VB6.Format(0, "00000000000000")
		mMainString = mMainString & mString
		
		'''28
		mString = New String(" ", 10)
		mMainString = mMainString & mString
		
		'''29
		mString = VB6.Format(0, "00000000000000")
		mMainString = mMainString & mString
		
		'''30
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
		
		SqlStr = "Select COUNT(CHALLANNO) TOTCHALLANNO, SUM(PAIDAMOUNT) AS TDSAMOUNT " & vbCrLf & " FROM TCS_CHALLAN TRN " & vbCrLf & " WHERE "
		
		SqlStr = SqlStr & vbCrLf & " TRN.REFDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.REFDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
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
		
		SqlStr = " Select COUNT(1) AS TOTDEDUCTEE, " & vbCrLf & " SUM(TCSAMOUNT) TOTTDSAMOUNT "
		
		SqlStr = SqlStr & vbCrLf & " FROM TCS_TRN IH, TCS_CHALLAN TRN "
		
		SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=TRN.COMPANY_CODE" & vbCrLf & " AND IH.FYEAR=TRN.FYEAR" & vbCrLf & " AND IH.TCSCHALLANMKEY=TRN.MKEY" & vbCrLf & " AND IH.CANCELLED='N'" & vbCrLf & " AND TRN.REFDATE>='" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "'" & vbCrLf & " AND TRN.REFDATE<='" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "'"
		
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
	Private Function PrintDD(ByRef mLineCount As Integer) As Boolean
		On Error GoTo ErrPart
		Dim mTitle As String
		Dim mString As String
		Dim mMainString As String
		Dim cntRow As Integer
		Dim mRs As Double
		Dim mPaisa As Double
		
		With SprdViewAnnex
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				
				'''1
				.Col = 1
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
				mString = "206C"
				mString = mString & New String(" ", 5 - Len(mString))
				mMainString = mMainString & mString
				
				''6
				.Col = 1
				mString = VB6.Format(Trim(.Text), "00")
				mMainString = mMainString & mString
				
				''7
				.Col = 2
				mString = VB.Left(UCase(Trim(.Text)), 10)
				mString = Trim(mString) & New String(" ", 10 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				''8
				.Col = 3
				mString = VB.Left(UCase(Trim(.Text)), 75)
				mString = Trim(mString) & New String(" ", 75 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''9
				.Col = 4
				mString = VB.Left(UCase(Trim(.Text)), 25)
				mString = Trim(mString) & New String(" ", 25 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''10
				.Col = 5
				mString = VB.Left(UCase(Trim(.Text)), 25)
				mString = Trim(mString) & New String(" ", 25 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''11
				.Col = 6
				mString = VB.Left(UCase(Trim(.Text)), 25)
				mString = Trim(mString) & New String(" ", 25 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''12
				.Col = 7
				mString = VB.Left(UCase(Trim(.Text)), 25)
				mString = Trim(mString) & New String(" ", 25 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''13
				.Col = 8
				mString = VB.Left(UCase(Trim(.Text)), 25)
				mString = Trim(mString) & New String(" ", 25 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''14
				.Col = 9
				mString = VB6.Format(Val(.Text), "00")
				mMainString = mMainString & mString
				
				'''15
				.Col = 10
				mString = VB.Left(.Text, 6)
				mString = VB6.Format(Val(mString), "000000")
				mMainString = mMainString & mString
				
				'''16
				.Col = 11
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''17
				.Col = 12
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				
				'''18
				.Col = 13
				mString = VB6.Format(Trim(.Text), "DDMMYYYY")
				mMainString = mMainString & mString
				
				''19
				mString = New String(" ", 1)
				mMainString = mMainString & mString
				
				'''20
				.Col = 14
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "00") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''21
				mString = New String(" ", 1)
				mMainString = mMainString & mString
				
				'''22
				.Col = 15
				mRs = 0
				mPaisa = 0
				If Trim(.Text) <> "" Then
					mRs = CDbl(Mid(Trim(.Text), 1, InStr(1, Trim(.Text), ".") - 1))
					mPaisa = CDbl(Mid(Trim(.Text), InStr(1, Trim(.Text), ".") + 1))
				End If
				mString = VB6.Format(Val(CStr(mRs)), "000000000000") & VB6.Format(Val(CStr(mPaisa)), "00")
				mMainString = mMainString & mString
				
				'''23
				.Col = 16
				mString = VB6.Format(Trim(.Text), "DDMMYYYY")
				mMainString = mMainString & mString
				
				'''24
				.Col = 17
				mString = VB.Left(.Text, 7)
				mString = Trim(mString) & New String(" ", 7 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''25
				.Col = 18
				mString = VB6.Format(Trim(.Text), "DDMMYYYY") & New String(" ", 8 - Len(VB6.Format(Trim(.Text), "DDMMYYYY")))
				mMainString = mMainString & mString
				
				''26
				.Col = 19
				mString = VB.Left(.Text, 9)
				mString = Trim(mString) & New String(" ", 9 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''27
				.Col = 20
				mString = VB6.Format(Trim(.Text), "DDMMYYYY") & New String(" ", 8 - Len(VB6.Format(Trim(.Text), "DDMMYYYY")))
				mMainString = mMainString & mString
				
				'''28
				.Col = 21
				mString = VB.Left(Trim(.Text), 1)
				mString = Trim(mString) & New String(" ", 1 - Len(Trim(mString)))
				mMainString = mMainString & mString
				
				'''29
				mString = VB6.Format("0", "00000000000000")
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