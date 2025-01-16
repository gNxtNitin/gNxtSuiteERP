Option Strict Off
Option Explicit On
Friend Class frmParamProdSeqReport
	Inherits System.Windows.Forms.Form
	Dim FormLoaded As Boolean
	
	Dim GroupOnItem As Boolean
	
	Private Const RowHeight As Short = 12
	
	
	Private Const ColItemCode As Short = 1
	Private Const ColItemName As Short = 2
	Private Const ColDept As Short = 3
	Private Const ColMin As Short = 4
	Private Const ColMax As Short = 5
	Private Const ColOperation As Short = 6
	Private Const ColOPRCode As Short = 7
	Private Const ColOldRate As Short = 8
	Private Const ColNewRate As Short = 9
	Private Const ColOtherRate As Short = 10
	Private Const ColOther2Rate As Short = 11
	Private Const ColOther3Rate As Short = 12
	Private Const ColWEF As Short = 13
	
	Dim CurrFormWidth As Integer
	Dim CurrFormHeight As Integer
	
	Dim mActiveRow As Integer
	Dim PrintFlag As Boolean
	'UPGRADE_WARNING: Event chkOPR.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkOPR_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOPR.CheckStateChanged
		txtOPRName.Enabled = IIf(chkOPR.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		cmdSearchOPR.Enabled = IIf(chkOPR.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		PrintStatus(False)
	End Sub
	'UPGRADE_WARNING: Event chkDept.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDept.CheckStateChanged
		PrintStatus(False)
		txtDept.Enabled = IIf(chkDept.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		cmdSearchDept.Enabled = IIf(chkDept.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		PrintStatus(False)
	End Sub
	
	'UPGRADE_WARNING: Event chkItemAll.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkItemAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkItemAll.CheckStateChanged
		txtItemName.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		cmdItemDesc.Enabled = IIf(chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
		PrintStatus(False)
	End Sub
	
	Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
		Me.Close()
	End Sub
	Private Sub FillSprdMain()
		On Error GoTo ERR1
		Dim I As Integer
		Dim mField As String
		
		With SprdMain
			.Row = 0
			
			.Col = 0
			.Text = "S.No."
			
			.Col = ColItemCode
			.Text = "Product Code"
			
			.Col = ColItemName
			.Text = "Product Name"
			
			.Col = ColDept
			.Text = "Department"
			
			.Col = ColMin
			.Text = "Min"
			
			.Col = ColMax
			.Text = "Max"
			
			.Col = ColOperation
			.Text = "Operation"
			
			.Col = ColOPRCode
			.Text = "Operation Code"
			
			.Col = ColOldRate
			.Text = "Old Rate Per 100"
			
			.Col = ColNewRate
			.Text = "New Rate Per 100"
			
			.Col = ColOtherRate
			.Text = "Other Rate Per 100"
			
			.Col = ColOther2Rate
			.Text = "Other II Rate Per 100"
			
			.Col = ColOther3Rate
			.Text = "Other III Rate Per 100"
			
			.Col = ColWEF
			.Text = "WEF"
		End With
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
		'Resume
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdMain(ByRef Arow As Integer)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim I As Integer
		Dim mColWidth As Integer
		
		With SprdMain
			.set_RowHeight(0, 2.5 * RowHeight)
			.Row = Arow
			.set_ColWidth(0, 5)
			.MaxCols = ColWEF
			
			.Col = ColItemCode
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColItemCode, 6.5)
			
			.Col = ColItemName
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColItemName, 30)
			
			.Col = ColDept
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColDept, 5)
			.ColHidden = False
			
			.Col = ColOperation
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColOperation, 28)
			.ColHidden = False
			
			.Col = ColOPRCode
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColOPRCode, 8)
			.ColHidden = False
			
			.Col = ColWEF
			.CellType = SS_CELL_TYPE_EDIT
			.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
			.TypeHAlign = SS_CELL_H_ALIGN_LEFT
			.set_RowHeight(Arow, RowHeight)
			.set_ColWidth(ColWEF, 8)
			.ColHidden = False
			
			For I = ColMin To ColMax
				.Col = I
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatMax = CDbl("999999999.99")
				.TypeFloatMin = CDbl("-999999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
				.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
				.set_ColWidth(I, 8)
			Next 
			
			For I = ColOldRate To ColOther3Rate
				.Col = I
				.CellType = SS_CELL_TYPE_FLOAT
				.TypeFloatDecimalChar = Asc(".")
				.TypeFloatMax = CDbl("999999999.99")
				.TypeFloatMin = CDbl("-999999999.99")
				.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
				.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
				.set_ColWidth(I, 8)
			Next 
			
			
			
			.ColsFrozen = ColItemName
			
			'        MainClass.SetSpreadColor SprdMain, -1
			'        MainClass.ProtectCell SprdMain, 1, .MaxRows, 1, .MaxCols
			'        .GridColor = &HC00000
			'        SprdMain.OperationMode = OperationModeSingle
			
			.Col = ColItemCode
			.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
			.Col = ColItemName
			.ColMerge = FPSpreadADO.MergeConstants.MergeAlways
			'        .Col = ColDept
			'        .ColMerge = MergeAlways
			'        .Col = ColItemCode
			'        .ColMerge = MergeAlways
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.SetSpreadColor(SprdMain, -1)
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ProtectCell. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
			SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
			
			SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
			SprdMain.DAutoCellTypes = True
			SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
			SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
			
		End With
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
		'Resume
	End Sub
	
	Private Sub cmdItemDesc_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdItemDesc.Click
		On Error GoTo ErrPart
		Dim SqlStr As String
		SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		
		'UPGRADE_WARNING: Untranslated statement in cmdItemDesc_Click. Please check source code.
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForStockOnHand(Crystal.DestinationConstants.crptToWindow)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ReportForStockOnHand(ByRef Mode As Crystal.DestinationConstants)
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		Dim mRPTName As String
		Dim mTitle As String
		Dim mSubTitle As String
		
		SqlStr = ""
		'UPGRADE_WARNING: Untranslated statement in ReportForStockOnHand. Please check source code.
		
		
		'''''Select Record for print...
		
		SqlStr = ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.FetchFromTempData. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
		
		
		mRPTName = "ProdSeqReport.rpt"
		mTitle = Me.Text
		
		If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			mSubTitle = "(Product : " & txtItemName.Text & ")"
		End If
		
		Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)
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
	Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
		Report1.SQLQuery = mSqlStr
		SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
		
		' Report1.CopiesToPrinter = PrintCopies
		Report1.WindowShowGroupTree = False
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
		Report1.Action = 1
	End Sub
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Call ReportForStockOnHand(Crystal.DestinationConstants.crptToPrinter)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub cmdSearchOPR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOPR.Click
		On Error GoTo ErrPart
		Dim SqlStr As String
		SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		
		'UPGRADE_WARNING: Untranslated statement in cmdSearchOPR_Click. Please check source code.
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
		On Error GoTo ErrPart
		Dim SqlStr As String
		SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
		
		'UPGRADE_WARNING: Untranslated statement in cmdSearchDept_Click. Please check source code.
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
		Dim MainClass_Renamed As Object
		
		Dim SqlStr As String
		PrintStatus(False)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ClearGrid(SprdMain)
		
		If FieldsVarification = False Then
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		
		Show1()
		'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		SprdMain.CtlRefresh()
		FormatSprdMain(-1)
		FillSprdMain()
		
		PrintStatus(True)
		'    SprdMain.SetFocus
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
		'Resume
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	Private Function FieldsVarification() As Boolean
		On Error GoTo err_Renamed
		Dim mOPRCode As String
		
		FieldsVarification = True
		
		If chkOPR.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			If Trim(txtOPRName.Text) = "" Then
				MsgInformation("Please Select Catgeory Name.")
				FieldsVarification = False
				txtOPRName.Focus()
			Else
				'UPGRADE_WARNING: Untranslated statement in FieldsVarification. Please check source code.
				
			End If
		End If
		
		Exit Function
err_Renamed: 
		MsgBox(Err.Description)
		''Resume
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function Show1() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo InsertErr
		Dim SqlStr As String
		Dim cntRow As Integer
		Dim mItemCode As String
		Dim mDeptCode As String
		Dim mOPRCode As String
		Dim pOldRate As Double
		Dim pNewRate As Double
		Dim pOtherRate As Double
		Dim pOther2Rate As Double
		Dim pOther3Rate As Double
		Dim pWEF As String
		
		SqlStr = MakeSQL
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, AData1, StrConn, "Y")
		
		With SprdMain
			For cntRow = 1 To .MaxRows
				.Row = cntRow
				.Col = ColItemCode
				mItemCode = Trim(.Text)
				
				.Col = ColDept
				mDeptCode = Trim(.Text)
				
				.Col = ColOPRCode
				mOPRCode = Trim(.Text)
				
				If GetOperationRate(mItemCode, mDeptCode, mOPRCode, pOldRate, pNewRate, pOtherRate, pOther2Rate, pOther3Rate, pWEF) = True Then
					.Row = cntRow
					.Col = ColOldRate
					.Text = VB6.Format(pOldRate, "0.00")
					
					.Col = ColNewRate
					.Text = VB6.Format(pNewRate, "0.00")
					
					.Col = ColOtherRate
					.Text = VB6.Format(pOtherRate, "0.00")
					
					.Col = ColOther2Rate
					.Text = VB6.Format(pOther2Rate, "0.00")
					
					.Col = ColOther3Rate
					.Text = VB6.Format(pOther3Rate, "0.00")
					
					.Col = ColWEF
					.Text = VB6.Format(pWEF, "DD/MM/YYYY")
					
				End If
			Next 
		End With
		
		Show1 = True
		Exit Function
InsertErr: 
		Show1 = False
		MsgBox(Err.Description)
		''Resume
	End Function
	
	
	
	Private Function MakeSQL() As String
		On Error GoTo InsertErr
		Dim SqlStr As String
		Dim mDeptCode As String
		Dim mOPRCode As String
		Dim mItemCode As String
		
		''
		''AND
		
		SqlStr = " SELECT ITEM.ITEM_CODE, ITEM.ITEM_SHORT_DESC,  " & vbCrLf & " IH.DEPT_CODE, IH.MIN_QTY, IH.MAX_QTY, " & vbCrLf & " TO_CHAR(OPR_SNO,'00') || '-' || PMST.OPR_DESC, TRN.OPR_CODE,0,0,0,0,0,'' "
		
		
		SqlStr = SqlStr & vbCrLf & " FROM PRD_PRODSEQUENCE_DET IH, " & vbCrLf & " PRD_OPR_TRN TRN, INV_ITEM_MST ITEM, PRD_OPR_MST PMST "
		
		
		''**********WHERE CLAUSE .......*************
		
		SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=TRN.COMPANY_CODE(+) AND IH.DEPT_CODE=TRN.DEPT_CODE(+) " & vbCrLf & " AND IH.PRODUCT_CODE=TRN.PRODUCT_CODE(+) AND IH.WEF=TRN.WEF(+)" & vbCrLf & " AND IH.COMPANY_CODE=ITEM.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=ITEM.ITEM_CODE "
		
		SqlStr = SqlStr & vbCrLf & " AND TRN.COMPANY_CODE=PMST.COMPANY_CODE(+)" & vbCrLf & " AND TRN.OPR_CODE=PMST.OPR_CODE(+) "
		
		If chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.
		End If
		
		If chkOPR.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.
		End If
		
		
		If chkItemAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
			'UPGRADE_WARNING: Untranslated statement in MakeSQL. Please check source code.
		End If
		
		SqlStr = SqlStr & vbCrLf & " AND IH.WEF = " & vbCrLf & " (SELECT MAX(WEF) " & vbCrLf & " FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE=IH.PRODUCT_CODE" & vbCrLf & " AND DEPT_CODE=IH.DEPT_CODE)"
		
		SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
		
		
		SqlStr = SqlStr & vbCrLf & "ORDER BY IH.PRODUCT_CODE, IH.SERIAL_NO, TRN.OPR_SNO "
		
		
		MakeSQL = SqlStr
		Exit Function
InsertErr: 
		MakeSQL = ""
		MsgBox(Err.Description)
		''Resume
	End Function
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function GetOperationRate(ByRef pItemCode As String, ByRef pDeptCode As String, ByRef pOperationCode As String, ByRef pOldRate As Double, ByRef pNewRate As Double, ByRef pOtherRate As Double, ByRef pOther2Rate As Double, ByRef pOther3Rate As Double, ByRef pWEF As String) As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo InsertErr
		Dim SqlStr As String
		Dim RsTemp As ADODB.Recordset
		
		
		pOldRate = 0
		pNewRate = 0
		pOtherRate = 0
		pOther2Rate = 0
		pOther3Rate = 0
		pWEF = ""
		GetOperationRate = False
		
		'UPGRADE_WARNING: Untranslated statement in GetOperationRate. Please check source code.
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
		
		If RsTemp.EOF = False Then
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pNewRate = IIf(IsDbNull(RsTemp.Fields("TOTAL_RATE").Value), 0, RsTemp.Fields("TOTAL_RATE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pOldRate = IIf(IsDbNull(RsTemp.Fields("TOTAL_OLD_RATE").Value), 0, RsTemp.Fields("TOTAL_OLD_RATE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pOtherRate = IIf(IsDbNull(RsTemp.Fields("TOTAL_OTHER_RATE").Value), 0, RsTemp.Fields("TOTAL_OTHER_RATE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pOther2Rate = IIf(IsDbNull(RsTemp.Fields("TOTAL_OTHER_2_RATE").Value), 0, RsTemp.Fields("TOTAL_OTHER_2_RATE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pOther3Rate = IIf(IsDbNull(RsTemp.Fields("TOTAL_OTHER_3_RATE").Value), 0, RsTemp.Fields("TOTAL_OTHER_3_RATE").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			pWEF = IIf(IsDbNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
		End If
		
		GetOperationRate = True
		Exit Function
InsertErr: 
		GetOperationRate = False
		''Resume
	End Function
	'UPGRADE_WARNING: Form event frmParamProdSeqReport.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmParamProdSeqReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		On Error GoTo ERR1
		
		Dim SqlStr As String
		If FormLoaded = True Then Exit Sub
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Me.Text = "Product Sequence Report"
		
		FormLoaded = True
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ERR1: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgInformation(Err.Description)
		'Resume
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event frmParamProdSeqReport.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmParamProdSeqReport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		Dim MainClass_Renamed As Object
		On Error GoTo ErrPart
		Dim mReFormWidth As Integer
		
		mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
		
		SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
		'    Frame4.Width = IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth)
		CurrFormWidth = mReFormWidth
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetSpreadColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetSpreadColor(SprdMain, -1)
		Exit Sub
ErrPart: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmParamProdSeqReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		On Error GoTo err_Renamed
		Dim mFromDate As String
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		
		CurrFormHeight = 7245
		CurrFormWidth = 11355
		
		Me.Top = 0
		Me.Left = 0
		Me.Height = VB6.TwipsToPixelsY(7245)
		Me.Width = VB6.TwipsToPixelsX(11355)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetControlsColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetControlsColor(Me)
		
		
		'    chkOPR.Value = vbChecked
		'    txtOPRName.Enabled = False
		'    cmdSearchOPR.Enabled = False
		'
		
		FormatSprdMain(-1)
		FillSprdMain()
		FormLoaded = False
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
err_Renamed: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		MsgBox(Err.Description)
	End Sub
	Private Sub frmParamProdSeqReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		On Error Resume Next
		Me.Close()
	End Sub
	
	Private Sub PrintStatus(ByRef PrintFlag As Boolean)
		cmdPrint.Enabled = PrintFlag
		CmdPreview.Enabled = PrintFlag
	End Sub
	
	'UPGRADE_WARNING: Event txtOPRName.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtOPRName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOPRName.TextChanged
		PrintStatus(False)
	End Sub
	Private Sub txtOPRName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOPRName.DoubleClick
		Call cmdSearchOPR_Click(cmdSearchOPR, New System.EventArgs())
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtOPRName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOPRName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtOPRName.Text)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtOPRName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOPRName.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchOPR_Click(cmdSearchOPR, New System.EventArgs())
	End Sub
	
	Private Sub txtOPRName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOPRName.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtOPRName.Text) = "" Then
			GoTo EventExitSub
		End If
		
		'UPGRADE_WARNING: Untranslated statement in txtOPRName_Validate. Please check source code.
		
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	
	
	
	'UPGRADE_WARNING: Event txtDept.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged
		PrintStatus(False)
	End Sub
	
	Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
		Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
	End Sub
	
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
	End Sub
	
	Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtDept.Text) = "" Then
			GoTo EventExitSub
		End If
		
		'UPGRADE_WARNING: Untranslated statement in txtDept_Validate. Please check source code.
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_WARNING: Event txtItemName.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged
		PrintStatus(False)
	End Sub
	
	Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
		Call cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		If KeyCode = System.Windows.Forms.Keys.F1 Then cmdItemDesc_Click(cmdItemDesc, New System.EventArgs())
	End Sub
	
	Private Sub txtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(txtItemName.Text) = "" Then
			GoTo EventExitSub
		End If
		
		'UPGRADE_WARNING: Untranslated statement in txtItemName_Validate. Please check source code.
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
End Class