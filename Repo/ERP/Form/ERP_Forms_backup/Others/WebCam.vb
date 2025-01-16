Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Module WebCam
	Public Const WS_CHILD As Integer = &H40000000
	Public Const WS_VISIBLE As Integer = &H10000000
	
	Public Const SWP_NOSIZE As Integer = &H1
	Public Const SWP_NOMOVE As Integer = &H2
	Public Const SWP_NOZORDER As Integer = &H4
	Public Const SWP_NOSENDCHANGING As Integer = &H400 ' /* Don't send WM_WINDOWPOSCHANGING */
	Public Const HWND_BOTTOM As Integer = 1
	
	
	Public Const WM_USER As Integer = &H400
	Public Const WM_CAP_START As Integer = WM_USER
	
	Public Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP_START + 10
	Public Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP_START + 11
	Public Const WM_CAP_SET_PREVIEW As Integer = WM_CAP_START + 50
	Public Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP_START + 52
	Public Const WM_CAP_DLG_VIDEOFORMAT As Integer = WM_CAP_START + 41
	Public Const WM_CAP_FILE_SAVEDIB As Integer = WM_CAP_START + 25
	
	Public Const WM_CAP_GET_FRAME As Integer = 1084
	Public Const WM_CAP_COPY As Integer = 1054
	
	Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
	
	
	Public Declare Function SendMessageAsLong Lib "user32"  Alias "SendMessageA"(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	
	Public Declare Function capCreateCaptureWindow Lib "avicap32.dll"  Alias "capCreateCaptureWindowA"(ByVal lpszWindowName As String, ByVal dwStyle As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hwndParent As Integer, ByVal nID As Integer) As Integer


    'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    Public Declare Function SendCamMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Object) As Integer

    'Public Const ws_child As Long = &H40000000
    'Public Const ws_visible As Long = &H10000000
    '
    'Public Const WM_USER = 1024
    'Public Const wm_cap_driver_connect = WM_USER + 10
    'Public Const wm_cap_set_preview = WM_USER + 50
    'Public Const WM_CAP_SET_PREVIEWRATE = WM_USER + 52
    'Public Const WM_CAP_DRIVER_DISCONNECT As Long = WM_USER + 11
    'Public Const WM_CAP_DLG_VIDEOFORMAT As Long = WM_USER + 41
    '
    'Public Declare Function SendCamMessage Lib "user32" Alias "SendMessageA" ( _
    ''    ByVal hWnd As Long, ByVal wMsg As Long, ByVal wParam As Long, _
    ''    ByVal lParam As Long) As Long
    '
    'Public Declare Function capCreateCaptureWindow Lib "avicap32.dll" Alias _
    ''    "capCreateCaptureWindowA" ( _
    ''    ByVal a As String, ByVal b As Long, ByVal c As Integer, _
    ''    ByVal d As Integer, ByVal e As Integer, ByVal f As Integer, _
    ''    ByVal g As Long, ByVal h As Integer) As Long

    Structure VFWPOINT 'strange name to avoid collision with other POINT UDTs
		Dim x As Integer
		Dim y As Integer
	End Structure
	
	Structure CAPSTATUS
		Dim uiImageWidth As Integer '// Width of the image
		Dim uiImageHeight As Integer '// Height of the image
		Dim fLiveWindow As Integer '// Now Previewing video?
		Dim fOverlayWindow As Integer '// Now Overlaying video?
		Dim fScale As Integer '// Scale image to client?
		Dim ptScroll As VFWPOINT '// Scroll position
		Dim fUsingDefaultPalette As Integer '// Using default driver palette?
		Dim fAudioHardware As Integer '// Audio hardware present?
		Dim fCapFileExists As Integer '// Does capture file exist?
		Dim dwCurrentVideoFrame As Integer '// # of video frames cap'td
		Dim dwCurrentVideoFramesDropped As Integer '// # of video frames dropped
		Dim dwCurrentWaveSamples As Integer '// # of wave samples cap'td
		Dim dwCurrentTimeElapsedMS As Integer '// Elapsed capture duration
		Dim hPalCurrent As Integer '// Current palette in use
		Dim fCapturingNow As Integer '// Capture in progress?
		Dim dwReturn As Integer '// Error value after any operation
		Dim wNumVideoAllocated As Integer '// Actual number of video buffers
		Dim wNumAudioAllocated As Integer '// Actual number of audio buffers
	End Structure
	
	Const BLOCK_SIZE As Short = 16384
	
	Public Sub BlobToFile(ByRef fld As ADODB.Field, ByVal FName As String, Optional ByRef FieldSize As Integer = -1, Optional ByRef Threshold As Integer = 1048576)
		'
		' Assumes file does not exist
		' Data cannot exceed approx. 2Gb in size
		'
		Dim f As Integer
		Dim bData() As Byte
		Dim sData As String
		f = FreeFile
		FileOpen(f, FName, OpenMode.Binary)
		Select Case fld.Type
			Case ADODB.DataTypeEnum.adLongVarBinary
				If FieldSize = -1 Then ' blob field is of unknown size
					WriteFromUnsizedBinary(f, fld)
				Else ' blob field is of known size
					If FieldSize > Threshold Then ' very large actual data
						WriteFromBinary(f, fld, FieldSize)
					Else ' smallish actual data
						bData = VB6.CopyArray(fld.Value)
						'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						FilePut(f, bData) ' PUT tacks on overhead if use fld.Value
					End If
				End If
			Case ADODB.DataTypeEnum.adLongVarChar, ADODB.DataTypeEnum.adLongVarWChar
				If FieldSize = -1 Then
					WriteFromUnsizedText(f, fld)
				Else
					If FieldSize > Threshold Then
						WriteFromText(f, fld, FieldSize)
					Else
						sData = fld.Value
						'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						FilePut(f, sData) ' PUT tacks on overhead if use fld.Value
					End If
				End If
		End Select
		FileClose(f)
	End Sub
	
	Public Sub WriteFromBinary(ByVal f As Integer, ByRef fld As ADODB.Field, ByVal FieldSize As Integer)
		Dim Data() As Byte
		Dim BytesRead As Integer
		Do While FieldSize <> BytesRead
			If FieldSize - BytesRead < BLOCK_SIZE Then
				'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Data = fld.GetChunk(FieldSize - BLOCK_SIZE)
				BytesRead = FieldSize
			Else
				'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Data = fld.GetChunk(BLOCK_SIZE)
				BytesRead = BytesRead + BLOCK_SIZE
			End If
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(f, Data)
		Loop 
	End Sub
	
	Public Sub WriteFromUnsizedBinary(ByVal f As Integer, ByRef fld As ADODB.Field)
		Dim Data() As Byte
		Dim temp As Object
        Do
            'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            temp = fld.GetChunk(BLOCK_SIZE)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            If IsDbNull(temp) Then Exit Do
            'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Data = temp
            'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            FilePut(f, Data)
            'UPGRADE_ISSUE: LenB function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        Loop While Len(temp) = BLOCK_SIZE
    End Sub
	
	Public Sub WriteFromText(ByVal f As Integer, ByRef fld As ADODB.Field, ByVal FieldSize As Integer)
		Dim Data As String
		Dim CharsRead As Integer
		Do While FieldSize <> CharsRead
			If FieldSize - CharsRead < BLOCK_SIZE Then
				'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Data = fld.GetChunk(FieldSize - BLOCK_SIZE)
				CharsRead = FieldSize
			Else
				'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Data = fld.GetChunk(BLOCK_SIZE)
				CharsRead = CharsRead + BLOCK_SIZE
			End If
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(f, Data)
		Loop 
	End Sub
	
	Public Sub WriteFromUnsizedText(ByVal f As Integer, ByRef fld As ADODB.Field)
		Dim Data As String
		Dim temp As Object
		Do 
			'UPGRADE_WARNING: Couldn't resolve default property of object fld.GetChunk(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			temp = fld.GetChunk(BLOCK_SIZE)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(temp) Then Exit Do
			'UPGRADE_WARNING: Couldn't resolve default property of object temp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Data = temp
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(f, Data)
		Loop While Len(temp) = BLOCK_SIZE
	End Sub
	
	Public Sub FileToBlob(ByVal FName As String, ByRef fld As ADODB.Field, Optional ByRef Threshold As Integer = 1048576)
		'
		' Assumes file exists
		' Assumes calling routine does the UPDATE
		' File cannot exceed approx. 2Gb in size
		'
		Dim f, FileSize As Integer
		Dim Data() As Byte
		f = FreeFile
		FileOpen(f, FName, OpenMode.Binary)
		FileSize = LOF(f)
		Select Case fld.Type
			Case ADODB.DataTypeEnum.adLongVarBinary
				If FileSize > Threshold Then
					ReadToBinary(f, fld, FileSize)
				Else
                    'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
                    'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
                    Data = System.Text.UnicodeEncoding.Unicode.GetBytes(InputBox(FileSize, f))
                    'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetString() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
                    fld.Value = System.Text.UnicodeEncoding.Unicode.GetString(Data)
				End If
			Case ADODB.DataTypeEnum.adLongVarChar, ADODB.DataTypeEnum.adLongVarWChar
				If FileSize > Threshold Then
					ReadToText(f, fld, FileSize)
				Else
					fld.Value = InputString(f, FileSize)
				End If
		End Select
		FileClose(f)
	End Sub
	
	Public Sub ReadToBinary(ByVal f As Integer, ByRef fld As ADODB.Field, ByVal FileSize As Integer)
		Dim Data() As Byte
		Dim BytesRead As Integer
		Do While FileSize <> BytesRead
			If FileSize - BytesRead < BLOCK_SIZE Then
                'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
                'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
                Data = System.Text.UnicodeEncoding.Unicode.GetBytes(InputBox(FileSize - BytesRead, f))    '' System.Text.UnicodeEncoding.Unicode.GetBytes(InputB(FileSize - BytesRead, f))
                BytesRead = FileSize
			Else
                'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
                'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
                Data = System.Text.UnicodeEncoding.Unicode.GetBytes(InputBox(BLOCK_SIZE, f))
                BytesRead = BytesRead + BLOCK_SIZE
			End If
			fld.AppendChunk(Data)
		Loop 
	End Sub
	
	Public Sub ReadToText(ByVal f As Integer, ByRef fld As ADODB.Field, ByVal FileSize As Integer)
		Dim Data As String
		Dim CharsRead As Integer
		Do While FileSize <> CharsRead
			If FileSize - CharsRead < BLOCK_SIZE Then
				Data = InputString(f, FileSize - CharsRead)
				CharsRead = FileSize
			Else
				Data = InputString(f, BLOCK_SIZE)
				CharsRead = CharsRead + BLOCK_SIZE
			End If
			fld.AppendChunk(Data)
		Loop 
	End Sub
End Module