Option Strict Off
Option Explicit On
Friend Class frmTCSeRTNReceiptEntry
	Inherits System.Windows.Forms.Form
	Dim RsTDSeRTN As ADODB.Recordset
	Dim ADDMode As Boolean
	Dim MODIFYMode As Boolean
	Dim XRIGHT As String
	'Dim PvtDBCn As ADODB.Connection
	Dim FormActive As Boolean
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub ViewGrid()
		Dim MainClass_Renamed As Object
		If CmdView.Text = ConCmdGridViewCaption Then
			CmdView.Text = ConCmdViewCaption
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ClearGrid. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ClearGrid(SprdView)
			AssignGrid(True)
			'        ADataMain.Refresh
			FormatSprdView()
			'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
			SprdView.CtlRefresh()
			
			SprdView.Focus()
			Fragridview.BringToFront()
		Else
			CmdView.Text = ConCmdGridViewCaption
			Fragridview.SendToBack()
		End If
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RsTDSeRTN, ADDMode, MODIFYMode, True)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Clear1()
		Dim MainClass_Renamed As Object
		
		TxtIQTRNo.Text = ""
		TxtIIQTRNo.Text = ""
		TxtIIIQTRNo.Text = ""
		TxtIVQTRNo.Text = ""
		TxtIQTRDate.Text = ""
		TxtIIQTRDate.Text = ""
		TxtIIIQTRDate.Text = ""
		TxtIVQTRDate.Text = ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RsTDSeRTN, ADDMode, MODIFYMode, True)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
		Dim MainClass_Renamed As Object
		If CmdModify.Text = ConcmdmodifyCaption Then
			ADDMode = False
			MODIFYMode = True
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainClass.ButtonStatus(Me, XRIGHT, RsTDSeRTN, ADDMode, MODIFYMode, True)
		Else
			ADDMode = False
			MODIFYMode = False
			Show1()
		End If
	End Sub
	
	Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
		Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
		ShowReport(Crystal.DestinationConstants.crptToWindow)
		Me.Cursor = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
		Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
		ShowReport(Crystal.DestinationConstants.crptToPrinter)
		Me.Cursor = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
		ViewGrid()
	End Sub
	Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
		If CmdAdd.Text = ConCmdAddCaption Then
			ADDMode = True
			MODIFYMode = False
			Clear1()
		Else
			ADDMode = False
			MODIFYMode = False
			If RsTDSeRTN.EOF = False Then RsTDSeRTN.MoveFirst()
			Show1()
		End If
	End Sub
	Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
		Me.Close()
	End Sub
	Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
		On Error GoTo DelErrPart
		'    If txtTDSName.Text = "" Then MsgExclamation "Nothing to delete": Exit Sub
		If Not RsTDSeRTN.EOF Then
			If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
				If Delete1 = False Then GoTo DelErrPart
				If RsTDSeRTN.EOF = True Then
					Clear1()
				Else
					Clear1()
					Show1()
				End If
			End If
		End If
		Exit Sub
DelErrPart: 
		MsgBox("Record Not Deleted")
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmTCSeRTNReceiptEntry_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.DoFunctionKey. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.DoFunctionKey(Me, KeyCode)
	End Sub
	Private Sub frmTCSeRTNReceiptEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
		Show1()
		CmdView_Click(CmdView, New System.EventArgs())
	End Sub
	
	Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
		If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIQTRDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIQTRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIQTRDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	Private Sub TxtIQTRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtIQTRDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(TxtIQTRDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(TxtIQTRDate.Text) Then
			MsgBox("Invalid Date.", MsgBoxStyle.Information)
			Cancel = True
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIIQTRDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIIQTRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIIQTRDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	Private Sub TxtIIQTRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtIIQTRDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(TxtIIQTRDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(TxtIIQTRDate.Text) Then
			MsgBox("Invalid Date.", MsgBoxStyle.Information)
			Cancel = True
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIIIQTRDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIIIQTRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIIIQTRDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	Private Sub TxtIIIQTRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtIIIQTRDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(TxtIIIQTRDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(TxtIIIQTRDate.Text) Then
			MsgBox("Invalid Date.", MsgBoxStyle.Information)
			Cancel = True
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIVQTRDate.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIVQTRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIVQTRDate.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	Private Sub TxtIVQTRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtIVQTRDate.Validating
		Dim Cancel As Boolean = eventArgs.Cancel
		If Trim(TxtIVQTRDate.Text) = "" Then GoTo EventExitSub
		
		If Not IsDate(TxtIVQTRDate.Text) Then
			MsgBox("Invalid Date.", MsgBoxStyle.Information)
			Cancel = True
		End If
EventExitSub: 
		eventArgs.Cancel = Cancel
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub TxtIQTRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtIQTRNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtIQTRNo)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIQTRNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIQTRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIQTRNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub TxtIIQTRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtIIQTRNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtIIQTRNo)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIIQTRNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIIQTRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIIQTRNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub TxtIIIQTRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtIIIQTRNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtIIIQTRNo)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIIIQTRNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIIIQTRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIIIQTRNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub TxtIVQTRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtIVQTRNo.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtIVQTRNo)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event TxtIVQTRNo.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub TxtIVQTRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtIVQTRNo.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Form event frmTCSeRTNReceiptEntry.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmTCSeRTNReceiptEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		Dim MainClass_Renamed As Object
		On Error GoTo ERR1
		Dim SqlStr As String
		
		If FormActive = True Then Exit Sub
		SqlStr = "SELECT * FROM TCS_RTN_TRN WHERE  1<>1"
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSeRTN, ADODB.LockTypeEnum.adLockReadOnly)
		Clear1()
		
		Call AssignGrid(False)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		SetTextLength()
		
		Show1()
		
		If RsTDSeRTN.EOF = True Then
			If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
		End If
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
	Private Sub frmTCSeRTNReceiptEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
		Me.Height = VB6.TwipsToPixelsY(3630)
		Me.Width = VB6.TwipsToPixelsX(8355)
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Exit Sub
ErrPart: 
		MsgBox(Err.Description)
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	Private Sub frmTCSeRTNReceiptEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		On Error Resume Next
		FormActive = False
		'UPGRADE_NOTE: Object RsTDSeRTN may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		RsTDSeRTN = Nothing
		'UPGRADE_NOTE: Object frmTDSSection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		frmTDSSection = Nothing
		'    PubDBCn.Cancel
		'    PvtDBCn.Close
		'    Set PvtDBCn = Nothing
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Show1()
		Dim MainClass_Renamed As Object
		On Error GoTo ShowErrPart
		Dim SqlStr As String
		
		SqlStr = "SELECT * " & vbCrLf & " FROM TCS_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UOpenRecordSet. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSeRTN, ADODB.LockTypeEnum.adLockReadOnly)
		
		If Not RsTDSeRTN.EOF Then
			
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIQTRNo.Text = IIf(IsDbNull(RsTDSeRTN.Fields("I_QTR_NO").Value), "", RsTDSeRTN.Fields("I_QTR_NO").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIIQTRNo.Text = IIf(IsDbNull(RsTDSeRTN.Fields("II_QTR_NO").Value), "", RsTDSeRTN.Fields("II_QTR_NO").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIIIQTRNo.Text = IIf(IsDbNull(RsTDSeRTN.Fields("III_QTR_NO").Value), "", RsTDSeRTN.Fields("III_QTR_NO").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIVQTRNo.Text = IIf(IsDbNull(RsTDSeRTN.Fields("IV_QTR_NO").Value), "", RsTDSeRTN.Fields("IV_QTR_NO").Value)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIQTRDate.Text = VB6.Format(IIf(IsDbNull(RsTDSeRTN.Fields("I_QTR_DATE").Value), "", RsTDSeRTN.Fields("I_QTR_DATE").Value), "DD/MM/YYYY")
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIIQTRDate.Text = VB6.Format(IIf(IsDbNull(RsTDSeRTN.Fields("II_QTR_DATE").Value), "", RsTDSeRTN.Fields("II_QTR_DATE").Value), "DD/MM/YYYY")
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIIIQTRDate.Text = VB6.Format(IIf(IsDbNull(RsTDSeRTN.Fields("III_QTR_DATE").Value), "", RsTDSeRTN.Fields("III_QTR_DATE").Value), "DD/MM/YYYY")
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			TxtIVQTRDate.Text = VB6.Format(IIf(IsDbNull(RsTDSeRTN.Fields("IV_QTR_DATE").Value), "", RsTDSeRTN.Fields("IV_QTR_DATE").Value), "DD/MM/YYYY")
			
			
			
		End If
		
		ADDMode = False
		MODIFYMode = False
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.ButtonStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.ButtonStatus(Me, XRIGHT, RsTDSeRTN, ADDMode, MODIFYMode, True)
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
			ADDMode = False
			MODIFYMode = False
			Show1()
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
		Dim SqlStr As String
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		
		If ADDMode = True Then
			
			'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
			
		Else
			
			'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
			
		End If
		
		PubDBCn.Execute(SqlStr)
		
		PubDBCn.CommitTrans()
		Update1 = True
		Exit Function
UpdateError: 
		Update1 = False
		PubDBCn.RollbackTrans()
		'    MsgBox err.Description + " Error No.: " + Str(err.Number)
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
		PubDBCn.Errors.Clear()
		RsTDSeRTN.Requery()
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Function
	Private Function FieldsVarification() As Boolean
		On Error GoTo ERR1
		
		FieldsVarification = True
		
		If ADDMode = False And MODIFYMode = False Then
			MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
			FieldsVarification = False
			Exit Function
		End If
		
		
		If TxtIQTRNo.Text <> "" Then
			'        If Len(TxtIQTRNo.Text) <> 15 Then
			'            MsgInformation "Receipt No Length must be 15."
			'            TxtIQTRNo.SetFocus
			'            FieldsVarification = False
			'            Exit Function
			'        End If
			If Trim(TxtIQTRDate.Text) = "" Then
				MsgInformation("Receipt Date Cann't be Blank.")
				TxtIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
			
			If Not IsDate(TxtIQTRDate.Text) Then
				MsgBox("Invalid Date.", MsgBoxStyle.Information)
				TxtIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
		End If
		
		If TxtIIQTRNo.Text <> "" Then
			'        If Len(TxtIIQTRNo.Text) <> 15 Then
			'            MsgInformation "Receipt No Length must be 15."
			'            TxtIIQTRNo.SetFocus
			'            FieldsVarification = False
			'            Exit Function
			'        End If
			If Trim(TxtIIQTRDate.Text) = "" Then
				MsgInformation("Receipt Date Cann't be Blank.")
				TxtIIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
			
			If Not IsDate(TxtIIQTRDate.Text) Then
				MsgBox("Invalid Date.", MsgBoxStyle.Information)
				TxtIIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
		End If
		
		If TxtIIIQTRNo.Text <> "" Then
			'        If Len(TxtIIIQTRNo.Text) <> 15 Then
			'            MsgInformation "Receipt No Length must be 15."
			'            TxtIIIQTRNo.SetFocus
			'            FieldsVarification = False
			'            Exit Function
			'        End If
			If Trim(TxtIIIQTRDate.Text) = "" Then
				MsgInformation("Receipt Date Cann't be Blank.")
				TxtIIIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
			
			If Not IsDate(TxtIIIQTRDate.Text) Then
				MsgBox("Invalid Date.", MsgBoxStyle.Information)
				TxtIIIQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
		End If
		
		If TxtIVQTRNo.Text <> "" Then
			'        If Len(TxtIVQTRNo.Text) <> 15 Then
			'            MsgInformation "Receipt No Length must be 15."
			'            TxtIVQTRNo.SetFocus
			'            FieldsVarification = False
			'            Exit Function
			'        End If
			If Trim(TxtIVQTRDate.Text) = "" Then
				MsgInformation("Receipt Date Cann't be Blank.")
				TxtIVQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
			
			If Not IsDate(TxtIVQTRDate.Text) Then
				MsgBox("Invalid Date.", MsgBoxStyle.Information)
				TxtIVQTRDate.Focus()
				FieldsVarification = False
				Exit Function
			End If
		End If
		
		
		If MODIFYMode = True And RsTDSeRTN.EOF = True Then Exit Function
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		Exit Function
ERR1: 
		MsgInformation(Err.Description)
	End Function
	Private Sub SetTextLength()
		On Error GoTo ERR1
		
		'UPGRADE_WARNING: TextBox property TxtIQTRNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIQTRNo.Maxlength = RsTDSeRTN.Fields("I_QTR_NO").DefinedSize
		'UPGRADE_WARNING: TextBox property TxtIIQTRNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIIQTRNo.Maxlength = RsTDSeRTN.Fields("II_QTR_NO").DefinedSize
		'UPGRADE_WARNING: TextBox property TxtIIIQTRNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIIIQTRNo.Maxlength = RsTDSeRTN.Fields("III_QTR_NO").DefinedSize
		'UPGRADE_WARNING: TextBox property TxtIVQTRNo.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIVQTRNo.Maxlength = RsTDSeRTN.Fields("IV_QTR_NO").DefinedSize
		'UPGRADE_WARNING: TextBox property TxtIQTRDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIQTRDate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property TxtIIQTRDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIIQTRDate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property TxtIIIQTRDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIIIQTRDate.Maxlength = 10
		'UPGRADE_WARNING: TextBox property TxtIVQTRDate.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtIVQTRDate.Maxlength = 10
		
		Exit Sub
ERR1: 
		MsgBox(Err.Description)
		'' Resume
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub AssignGrid(ByRef mRefresh As Boolean)
		Dim MainClass_Renamed As Object
		Dim SqlStr As String
		
		SqlStr = " SELECT I_QTR_NO, II_QTR_NO, III_QTR_NO, IV_QTR_NO" & vbCrLf & " FROM TCS_RTN_TRN TRN " & vbCrLf & " WHERE TRN.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR = " & RsCompany.Fields("FYEAR").Value & ""
		
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AssignDataInSprd. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.AssignDataInSprd(SqlStr, ADataGrid, StrConn, IIf(mRefresh = True, "Y", "N"))
		FormatSprdView()
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub FormatSprdView()
		Dim MainClass_Renamed As Object
		With SprdView
			.Row = -1
			.set_RowHeight(0, 12)
			.set_ColWidth(0, 5)
			.set_ColWidth(1, 12)
			.set_ColWidth(2, 12)
			.set_ColWidth(3, 12)
			.set_ColWidth(4, 12)
			
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
	
	Private Function Delete1() As Boolean
		On Error GoTo DeleteErr
		Dim SqlStr As String
		
		SqlStr = ""
		'     If IsFieldExist = True Then Delete1 = False: Exit Function
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		If InsertIntoDelAudit(PubDBCn, "TCS_RTN_TRN", (TxtIQTRNo.Text), RsTDSeRTN) = False Then GoTo DeleteErr
		If InsertIntoDeleteTrn(PubDBCn, "TCS_RTN_TRN", "FYEAR", RsCompany.Fields("FYEAR").Value) = False Then GoTo DeleteErr
		
		SqlStr = " DELETE " & vbCrLf & " FROM TCS_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
		
		PubDBCn.Execute(SqlStr)
		
		PubDBCn.CommitTrans()
		RsTDSeRTN.Requery()
		Delete1 = True
		Exit Function
DeleteErr: 
		Delete1 = False
		PubDBCn.RollbackTrans()
		RsTDSeRTN.Requery()
		If Err.Number = -2147467259 Then
			MsgBox("Can't Delete Transaction Exists Against this Code")
			Exit Function
		End If
		MsgBox(Err.Description)
	End Function
	
	Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
		On Error GoTo ERR1
		Dim mTitle As String
		Dim mSubTitle As String
		
		Report1.Reset()
		mTitle = "TDS Return For 4 Qty"
		mSubTitle = "From : " & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY") & " TO : " & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
		
		Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\TDS_RTN.rpt"
		SetCrpt(Report1, Mode, 1, mTitle, mSubTitle)
		Report1.WindowShowGroupTree = False
		Report1.Action = 1
		Exit Sub
ERR1: 
		MsgInformation(Err.Description)
	End Sub
End Class