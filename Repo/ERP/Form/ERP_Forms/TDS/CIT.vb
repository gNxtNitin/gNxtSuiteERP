Option Strict Off
Option Explicit On
Friend Class frmCIT
	Inherits System.Windows.Forms.Form
	
	Dim XRIGHT As String
	Dim ADDMode As Boolean
	Dim MODIFYMode As Boolean
	Dim PostTaxWiseSale As Byte
	Dim PostSaleAcCode As Integer
	
	Private Const ConRowHeight As Short = 12
	
	
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		'UPGRADE_NOTE: Object frmeAuthorised may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		frmeAuthorised = Nothing
	End Sub
	Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
		If FieldVerification = False Then Exit Sub
		If Update1 = True Then CmdSave.Enabled = False
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmCIT_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		Call SetMainFormCordinate(frmeAuthorised)
		
		''Set PvtDBCn = New ADODB.Connection
		''PvtDBCn.Open StrConn
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.STRMenuRight. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.RightsToButton. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.RightsToButton(Me, XRIGHT)
		SetMaxLength()
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetControlsColor. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SetControlsColor(Me)
		Me.Left = 0
		Me.Top = 0
		
		
		ADDMode = False
		MODIFYMode = False
		If XRIGHT <> "" Then MODIFYMode = True
		Show1()
	End Sub
	Private Sub SetMaxLength()
		
		Dim mAccountLength As Integer
		
		'UPGRADE_WARNING: TextBox property txtCircle.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtCircle.Maxlength = RsCompany.Fields("CIRCLE_NAME").DefinedSize
		'UPGRADE_WARNING: TextBox property txtaddress.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtaddress.Maxlength = RsCompany.Fields("CIRCLE_ADDRESS").DefinedSize
		'UPGRADE_WARNING: TextBox property TxtCity.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		TxtCity.Maxlength = RsCompany.Fields("CIRCLE_CITY").DefinedSize ''
		'UPGRADE_WARNING: TextBox property txtPin.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtPin.Maxlength = RsCompany.Fields("CIRCLE_PINCODE").DefinedSize
	End Sub
	Private Sub Show1()
		On Error GoTo ERR1
		ShowTDS()
		
		CmdSave.Enabled = False
		Exit Sub
ERR1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	Private Sub ShowTDS()
		On Error GoTo ERR1
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtCircle.Text = IIf(IsDbNull(RsCompany.Fields("CIRCLE_NAME").Value), "", RsCompany.Fields("CIRCLE_NAME").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtaddress.Text = IIf(IsDbNull(RsCompany.Fields("CIRCLE_ADDRESS").Value), "", RsCompany.Fields("CIRCLE_ADDRESS").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		TxtCity.Text = IIf(IsDbNull(RsCompany.Fields("CIRCLE_CITY").Value), "", RsCompany.Fields("CIRCLE_CITY").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtPin.Text = IIf(IsDbNull(RsCompany.Fields("CIRCLE_PINCODE").Value), "", RsCompany.Fields("CIRCLE_PINCODE").Value)
		Exit Sub
ERR1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Function Update1() As Boolean
		Dim MainClass_Renamed As Object
		On Error GoTo err_Renamed
		Dim SqlStr As String
		Dim xCode As Integer
		
		Dim mAddMode As Boolean
		
		PubDBCn.Errors.Clear()
		PubDBCn.BeginTrans()
		
		SqlStr = ""
		xCode = RsCompany.Fields("Company_Code").Value
		
		'UPGRADE_WARNING: Untranslated statement in Update1. Please check source code.
		
		If mAddMode = True Then
			SqlStr = "INSERT INTO FIN_PRINT_MST ( " & vbCrLf & " COMPANY_CODE, CIRCLE_NAME, CIRCLE_ADDRESS, " & vbCrLf & " CIRCLE_CITY, CIRCLE_PINCODE " & vbCrLf & " ) VALUES ( "
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = SqlStr & vbCrLf & " " & xCode & ", '" & MainClass.AllowSingleQuote(Trim(txtCircle.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtaddress.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(TxtCity.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtPin.Text)) & "') "
			
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = "UPDATE  FIN_PRINT_MST SET  " & vbCrLf & " CIRCLE_NAME = '" & MainClass.AllowSingleQuote(Trim(txtCircle.Text)) & "', " & vbCrLf & " CIRCLE_ADDRESS = '" & MainClass.AllowSingleQuote(Trim(txtaddress.Text)) & "', " & vbCrLf & " CIRCLE_CITY = '" & MainClass.AllowSingleQuote(Trim(TxtCity.Text)) & "', " & vbCrLf & " CIRCLE_PINCODE = '" & MainClass.AllowSingleQuote(Trim(txtPin.Text)) & "' "
			
			SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & xCode & ""
			
		End If
		
		PubDBCn.Execute(SqlStr)
		
		PubDBCn.CommitTrans()
		Update1 = True
		RsCompany.Requery() ''.Refresh
		
		Exit Function
err_Renamed: 
		Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
		Update1 = False
		PubDBCn.RollbackTrans() ''
		RsCompany.Requery() ''.Refresh
		
		
	End Function
	Private Sub frmCIT_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		Me.Close()
		'UPGRADE_NOTE: Object frmeAuthorised may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		frmeAuthorised = Nothing
	End Sub
	Private Function FieldVerification() As Boolean
		On Error GoTo ERR1
		FieldVerification = True
		
		
		Exit Function
ERR1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtAddress.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddress.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtaddress)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtCircle.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtCircle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCircle.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtCircle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCircle.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtCircle)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtCity.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCity.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, TxtCity.Text)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtPin.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtPin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPin.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtPin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPin.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SetNumericField. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.SetNumericField(KeyAscii)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class