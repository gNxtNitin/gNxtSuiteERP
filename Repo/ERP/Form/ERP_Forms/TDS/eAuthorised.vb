Option Strict Off
Option Explicit On
Friend Class frmeAuthorised
	Inherits System.Windows.Forms.Form
	
	Dim XRIGHT As String
	Dim ADDMode As Boolean
	Dim MODIFYMode As Boolean
	Dim PostTaxWiseSale As Byte
	Dim PostSaleAcCode As Integer
	
	Private Const ConRowHeight As Short = 12
	
	Private Const ColFromAccountName As Short = 1
	Private Const ColToAccountName As Short = 2
	
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		'UPGRADE_NOTE: Object frmeAuthorised may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Me = Nothing
	End Sub
	Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
		If FieldVerification = False Then Exit Sub
		If Update1 = True Then cmdSave.Enabled = False
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub frmeAuthorised_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim MainClass_Renamed As Object
		Call SetMainFormCordinate(Me)
		
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
		
		'UPGRADE_WARNING: TextBox property txtAuthorized.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtAuthorized.Maxlength = RsCompany.Fields("TDSAUTHORIZED").DefinedSize
		'UPGRADE_WARNING: TextBox property txtAuthorizedFName.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtAuthorizedFName.Maxlength = RsCompany.Fields("TDSAUTHORIZED_FNAME").DefinedSize
		'UPGRADE_WARNING: TextBox property txtDesignation.MaxLength has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		txtDesignation.Maxlength = RsCompany.Fields("TDSAUTHORIZED_DESIG").DefinedSize ''
		
	End Sub
	Private Sub Show1()
		On Error GoTo ERR1
		ShowTDS()
		
		cmdSave.Enabled = False
		Exit Sub
ERR1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Sub
	
	Private Sub ShowTDS()
		On Error GoTo ERR1
		
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtAuthorized.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtAuthorizedFName.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		txtDesignation.Text = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
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
			SqlStr = "INSERT INTO FIN_PRINT_MST ( " & vbCrLf & " COMPANY_CODE, TDSAUTHORIZED, TDSAUTHORIZED_FNAME, " & vbCrLf & " TDSAUTHORIZED_DESIG" & vbCrLf & " ) VALUES ( "
			
			
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = SqlStr & vbCrLf & " " & xCode & ", '" & MainClass.AllowSingleQuote(Trim(txtAuthorized.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtAuthorizedFName.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtDesignation.Text)) & "') "
			
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.AllowSingleQuote. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			SqlStr = "UPDATE  FIN_PRINT_MST SET  " & vbCrLf & " TDSAUTHORIZED = '" & MainClass.AllowSingleQuote(Trim(txtAuthorized.Text)) & "', " & vbCrLf & " TDSAUTHORIZED_FNAME = '" & MainClass.AllowSingleQuote(Trim(txtAuthorizedFName.Text)) & "', " & vbCrLf & " TDSAUTHORIZED_DESIG = '" & MainClass.AllowSingleQuote(Trim(txtDesignation.Text)) & "' "
			
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
	Private Sub frmeAuthorised_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		Me.Close()
		'UPGRADE_NOTE: Object frmeAuthorised may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Me = Nothing
	End Sub
	Private Function FieldVerification() As Boolean
		On Error GoTo ERR1
		FieldVerification = True
		
		
		Exit Function
ERR1: 
		ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
	End Function
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtAuthorized.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtAuthorized_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorized.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
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
	
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtAuthorizedFName.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtAuthorizedFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorizedFName.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtAuthorizedFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorizedFName.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorizedFName)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_WARNING: Event txtDesignation.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtDesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesignation.TextChanged
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.SaveStatus. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainClass.SaveStatus(Me, ADDMode, MODIFYMode)
	End Sub
	'UPGRADE_NOTE: MainClass was upgraded to MainClass_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub txtDesignation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesignation.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Dim MainClass_Renamed As Object
		'UPGRADE_WARNING: Couldn't resolve default property of object MainClass.UpperCase. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		KeyAscii = MainClass.UpperCase(KeyAscii, txtDesignation.Text)
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
End Class