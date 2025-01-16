Option Strict Off
Option Explicit On
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports VB = Microsoft.VisualBasic
Module modSendMail


    Public emailAdd As String
    Public outSourec As String
    Public fieldArr(76, 2) As String
    Public fText As String
    Public fRTF As String
    Public fonttbl() As String
    Public colortbl() As String
    Public mDEL As Boolean
    Public mSAll As Boolean
    Const boundary As String = "\plain"
    Const mfont As String = "\f"
    Const mfontsize As String = "\fs"
    Const bold As String = "\b"
    Const italic As String = "\i"
    Const underline As String = "\ul"
    Const para As String = "\par "
    Public Const start As String = "\deflang1033\pard\plain"
    Const finish As String = "\par }"

    Public Const CREATE_NEW As Short = 1
    Public Const CREATE_ALWAYS As Short = 2
    Public Const OPEN_EXISTING As Short = 3
    Public Const OPEN_ALWAYS As Short = 4
    Public Const TRUNCATE_EXISTING As Short = 5
    Public Const GENERIC_READ As Integer = &H80000000
    Public Const GENERIC_WRITE As Integer = &H40000000
    Public Const INVALID_HANDLE_VALUE As Short = -1
    Public Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
    Public Const MAX_PATH As Short = 260
    Public Const FILE_SHARE_WRITE As Integer = &H2
    Public Const FILE_SHARE_READ As Integer = &H1
    Public Const FILE_ATTRIBUTE_TEMPORARY As Integer = &H100
    Public Const ERROR_NO_MORE_FILES As Short = 18
    Public Const FW_NORMAL As Short = 400
    Public Const DEFAULT_CHARSET As Short = 1
    Public Const OUT_DEFAULT_PRECIS As Short = 0
    Public Const CLIP_DEFAULT_PRECIS As Short = 0
    Public Const DEFAULT_QUALITY As Short = 0
    Public Const DEFAULT_PITCH As Short = 0
    Public Const FF_ROMAN As Short = 16
    Public Const CF_PRINTERFONTS As Integer = &H2
    Public Const CF_SCREENFONTS As Integer = &H1
    Public Const CF_BOTH As Boolean = (CF_SCREENFONTS Or CF_PRINTERFONTS)
    Public Const CF_EFFECTS As Integer = &H100
    Public Const CF_FORCEFONTEXIST As Integer = &H10000
    Public Const CF_INITTOLOGFONTSTRUCT As Integer = &H40
    Public Const CF_LIMITSIZE As Integer = &H2000
    Public Const REGULAR_FONTTYPE As Integer = &H400
    Public Const LF_FACESIZE As Short = 32
    Public Const CCHDEVICENAME As Short = 32
    Public Const CCHFORMNAME As Short = 32
    Public Const GMEM_MOVEABLE As Integer = &H2
    Public Const GMEM_ZEROINIT As Integer = &H40
    Public Const DM_DUPLEX As Integer = &H1000
    Public Const DM_ORIENTATION As Integer = &H1
    Public Const PD_PRINTSETUP As Integer = &H40
    Public Const PD_DISABLEPRINTTOFILE As Integer = &H80000
    Public Const MF_CHECKED As Integer = &H8
    Public Const MF_APPEND As Integer = &H100
    Public Const TPM_LEFTALIGN As Integer = &H0
    Public Const MF_DISABLED As Integer = &H2
    Public Const MF_GRAYED As Integer = &H1
    Public Const MF_SEPARATOR As Integer = &H800
    Public Const MF_STRING As Integer = &H0
    Const REG_SZ As Short = 1
    Const REG_BINARY As Short = 3
    Const REG_DWORD As Short = 4
    Public Const HKEY_LOCAL_MACHINE As Integer = &H80000002
    Public Const HKEY_PERF_ROOT As Integer = HKEY_LOCAL_MACHINE
    Public Const HKEY_USERS As Integer = &H80000003
    Public Const HKEY_CLASSES_ROOT As Integer = &H80000000
    Public Const HKEY_CURRENT_CONFIG As Integer = &H80000005
    Public Const HKEY_CURRENT_USER As Integer = &H80000001
    Public Const HKEY_DYN_DATA As Integer = &H80000006

    Public Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure
    Public Structure RECT
        Dim Left_Renamed As Integer
        Dim Top As Integer
        Dim Right_Renamed As Integer
        Dim Bottom As Integer
    End Structure
    Public Structure OPENFILENAME
        Dim lStructSize As Integer
        Dim hwndOwner As Integer
        Dim hInstance As Integer
        Dim lpstrFilter As String
        Dim lpstrCustomFilter As String
        Dim nMaxCustFilter As Integer
        Dim nFilterIndex As Integer
        Dim lpstrFile As String
        Dim nMaxFile As Integer
        Dim lpstrFileTitle As String
        Dim nMaxFileTitle As Integer
        Dim lpstrInitialDir As String
        Dim lpstrTitle As String
        Dim flags As Integer
        Dim nFileOffset As Short
        Dim nFileExtension As Short
        Dim lpstrDefExt As String
        Dim lCustData As Integer
        Dim lpfnHook As Integer
        Dim lpTemplateName As String
    End Structure
    Public Structure SECURITY_ATTRIBUTES
        Dim nLength As Integer
        Dim lpSecurityDescriptor As Integer
        Dim bInheritHandle As Integer
    End Structure
    Public Structure FILETIME
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure
    Public Structure WIN32_FIND_DATA
        Dim dwFileAttributes As Integer
        Dim ftCreationTime As FILETIME
        Dim ftLastAccessTime As FILETIME
        Dim ftLastWriteTime As FILETIME
        Dim nFileSizeHigh As Integer
        Dim nFileSizeLow As Integer
        Dim dwReserved0 As Integer
        Dim dwReserved1 As Integer
        <VBFixedString(MAX_PATH), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=MAX_PATH)> Public cFileName() As Char
        <VBFixedString(14), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=14)> Public cAlternate() As Char
    End Structure
    Public Structure CHOOSECOLOR
        Dim lStructSize As Integer
        Dim hwndOwner As Integer
        Dim hInstance As Integer
        Dim rgbResult As Integer
        Dim lpCustColors As String
        Dim flags As Integer
        Dim lCustData As Integer
        Dim lpfnHook As Integer
        Dim lpTemplateName As String
    End Structure
    Public Structure LOGFONT
        Dim lfHeight As Integer
        Dim lfWidth As Integer
        Dim lfEscapement As Integer
        Dim lfOrientation As Integer
        Dim lfWeight As Integer
        Dim lfItalic As Byte
        Dim lfUnderline As Byte
        Dim lfStrikeOut As Byte
        Dim lfCharSet As Byte
        Dim lfOutPrecision As Byte
        Dim lfClipPrecision As Byte
        Dim lfQuality As Byte
        Dim lfPitchAndFamily As Byte
        <VBFixedString(31), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=31)> Public lfFaceName() As Char
    End Structure
    Public Structure CHOOSEFONT
        Dim lStructSize As Integer
        Dim hwndOwner As Integer
        Dim hDC As Integer
        Dim lpLogFont As Integer
        Dim iPointSize As Integer
        Dim flags As Integer
        Dim rgbColors As Integer
        Dim lCustData As Integer
        Dim lpfnHook As Integer
        Dim lpTemplateName As String
        Dim hInstance As Integer
        Dim lpszStyle As String
        Dim nFontType As Short
        Dim MISSING_ALIGNMENT As Short
        Dim nSizeMin As Integer
        Dim nSizeMax As Integer
    End Structure

    Public Declare Function CloseHandle Lib "kernel32" (ByVal hFile As Integer) As Integer
    Public Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, lpSecurityAttributes As Integer, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As Integer
    Public Declare Function ReadFile Lib "kernel32" (ByVal hFile As Integer, ByVal lpBuffer As Integer, ByVal nNumberOfBytesToRead As Integer, lpNumberOfBytesRead As Integer, ByVal lpOverlapped As Integer) As Integer
    Public Declare Function WriteFile Lib "kernel32" (ByVal hFile As Integer, ByVal lpBuffer As Integer, ByVal nNumberOfBytesToWrite As Integer, lpNumberOfBytesWritten As Integer, ByVal lpOverlapped As Integer) As Integer
    'Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)

    'Public Declare Function WriteFile Lib "kernel32" (ByVal hFile As Integer, ByRef lpBuffer As Any, ByVal nNumberOfBytesToWrite As Integer, ByRef lpNumberOfBytesWritten As Integer, ByVal lpOverlapped As Any) As Integer
    'Public Declare Function CreateFile Lib "kernel32" Alias "CreateFileA" (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, ByVal lpSecurityAttributes As Any, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As Integer
    'Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Public Declare Function FindFirstFile Lib "kernel32" Alias "FindFirstFileA" (ByVal lpFileName As String, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer
    Public Declare Function GetFileAttributes Lib "kernel32" Alias "GetFileAttributesA" (ByVal lpFileName As String) As Integer
    Public Declare Function FindClose Lib "kernel32" (ByVal hFindFile As Integer) As Integer
    Public Declare Function CreateDirectory Lib "kernel32" Alias "CreateDirectoryA" (ByVal lpPathName As String, ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES) As Integer
    'Public Declare Function ReadFile Lib "kernel32" (ByVal hFile As Integer, ByRef lpBuffer As Any, ByVal nNumberOfBytesToRead As Integer, ByRef lpNumberOfBytesRead As Integer, ByVal lpOverlapped As Any) As Integer
    Public Declare Function GetFileSize Lib "kernel32" (ByVal hFile As Integer, ByRef lpFileSizeHigh As Integer) As Integer
    Public Declare Function GetTempFileName Lib "kernel32" Alias "GetTempFileNameA" (ByVal lpszPath As String, ByVal lpPrefixString As String, ByVal wUnique As Integer, ByVal lpTempFileName As String) As Integer
    Public Declare Function SetFileAttributes Lib "kernel32" Alias "SetFileAttributesA" (ByVal lpFileName As String, ByVal dwFileAttributes As Integer) As Integer
    Public Declare Function DeleteFile Lib "kernel32" Alias "DeleteFileA" (ByVal lpFileName As String) As Integer
    Public Declare Function FindNextFile Lib "kernel32" Alias "FindNextFileA" (ByVal hFindFile As Integer, ByRef lpFindFileData As WIN32_FIND_DATA) As Integer
    Public Declare Function GetLastError Lib "kernel32" () As Integer
    Public Declare Function GetTempPath Lib "kernel32" Alias "GetTempPathA" (ByVal nBufferLength As Integer, ByVal lpBuffer As String) As Integer
    Public Declare Function MoveFile Lib "kernel32" Alias "MoveFileA" (ByVal lpExistingFileName As String, ByVal lpNewFileName As String) As Integer
    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Public Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
    Public Declare Function GetOpenFileName Lib "comdlg32.dll" Alias "GetOpenFileNameA" (ByRef pOpenfilename As OPENFILENAME) As Integer
    Public Declare Function GetSaveFileName Lib "comdlg32.dll" Alias "GetSaveFileNameA" (ByRef pOpenfilename As OPENFILENAME) As Integer
    Public Declare Function CHOOSEFONT_Renamed Lib "comdlg32.dll" Alias "ChooseFontA" (ByRef pChoosefont As CHOOSEFONT) As Integer
    Public Declare Function CHOOSECOLOR_Renamed Lib "comdlg32.dll" Alias "ChooseColorA" (ByRef pChoosecolor As CHOOSECOLOR) As Integer
    Public Declare Function GlobalLock Lib "kernel32" (ByVal hMem As Integer) As Integer
    Public Declare Function GlobalUnlock Lib "kernel32" (ByVal hMem As Integer) As Integer
    Public Declare Function GlobalAlloc Lib "kernel32" (ByVal wFlags As Integer, ByVal dwBytes As Integer) As Integer
    Public Declare Function GlobalFree Lib "kernel32" (ByVal hMem As Integer) As Integer
    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef hpvDest As Integer, ByRef hpvSource As LOGFONT, ByVal cbCopy As Integer)


    Public Declare Function WritePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Public Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Public Declare Function CreatePopupMenu Lib "user32" () As Integer
    Public Declare Function TrackPopupMenu Lib "user32" (ByVal hMenu As Integer, ByVal wFlags As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nReserved As Integer, ByVal hwnd As Integer, ByVal lprc As Integer) As Integer
    Public Declare Function CopyFile Lib "kernel32" Alias "CopyFileA" (ByVal lpExistingFileName As String, ByVal lpNewFileName As String, ByVal bFailIfExists As Integer) As Integer
    Public Declare Function SetFilePointer Lib "kernel32" (ByVal hFile As Integer, ByVal lDistanceToMove As Integer, ByRef lpDistanceToMoveHigh As Integer, ByVal dwMoveMethod As Integer) As Integer
    Private Declare Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Integer) As Integer
    Private Declare Function RegCreateKey Lib "advapi32.dll" Alias "RegCreateKeyA" (ByVal hKey As Integer, ByVal lpSubKey As String, ByRef phkResult As Integer) As Integer
    Private Declare Function RegDeleteValue Lib "advapi32.dll" Alias "RegDeleteValueA" (ByVal hKey As Integer, ByVal lpValueName As String) As Integer
    Private Declare Function RegOpenKey Lib "advapi32.dll" Alias "RegOpenKeyA" (ByVal hKey As Integer, ByVal lpSubKey As String, ByRef phkResult As Integer) As Integer
    Private Declare Function RegQueryValueEx Lib "advapi32.dll" Alias "RegQueryValueExA" (ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef lpData As Integer, ByRef lpcbData As Integer) As Integer
    Private Declare Function RegSetValueEx Lib "advapi32.dll" Alias "RegSetValueExA" (ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByRef lpData As Integer, ByVal cbData As Integer) As Integer

    Public Function CopyToOtherDIR(ByRef sFNam As String, ByRef dFNam As String) As Boolean
        If CopyFile(sFNam & Chr(0), dFNam & Chr(0), 0) <> 0 Then
            CopyToOtherDIR = True
        Else
            CopyToOtherDIR = False
        End If
    End Function

    Public Function ShowFont(ByRef hWind As Integer) As String
        'Dim Printer As New Printer
        Dim cf As CHOOSEFONT = Nothing
        Dim lfont As LOGFONT
        Dim hMem, pMem As Integer
        Dim retval As Integer

        ShowFont = ""

        lfont.lfHeight = 0
        lfont.lfWidth = 0
        lfont.lfEscapement = 0
        lfont.lfOrientation = 0
        lfont.lfWeight = FW_NORMAL
        lfont.lfCharSet = DEFAULT_CHARSET
        lfont.lfOutPrecision = OUT_DEFAULT_PRECIS
        lfont.lfClipPrecision = CLIP_DEFAULT_PRECIS
        lfont.lfQuality = DEFAULT_QUALITY
        lfont.lfPitchAndFamily = DEFAULT_PITCH Or FF_ROMAN
        lfont.lfFaceName = "Times New Roman" & vbNullChar
        hMem = GlobalAlloc(GMEM_MOVEABLE Or GMEM_ZEROINIT, Len(lfont))
        pMem = GlobalLock(hMem)
        CopyMemory(pMem, lfont, Len(lfont))
        cf.lStructSize = Len(cf)
        cf.hwndOwner = hWind
        'cf.hDC = Printer.hDC
        cf.lpLogFont = pMem
        cf.iPointSize = 120
        cf.flags = CF_BOTH Or CF_EFFECTS Or CF_FORCEFONTEXIST Or CF_INITTOLOGFONTSTRUCT Or CF_LIMITSIZE
        cf.rgbColors = RGB(0, 0, 0)
        cf.nFontType = REGULAR_FONTTYPE
        cf.nSizeMin = 10
        cf.nSizeMax = 72
        retval = CHOOSEFONT_Renamed(cf)
        If retval <> 0 Then
            CopyMemory(pMem, lfont, Len(lfont))
            ShowFont = Left(lfont.lfFaceName, InStr(lfont.lfFaceName, vbNullChar) - 1) & "#" & lfont.lfItalic & "#" & lfont.lfItalic
        End If
        retval = GlobalUnlock(hMem)
        retval = GlobalFree(hMem)
    End Function

    Public Function GetWINPath() As String
        Dim buff As String
        Dim l As Short

        buff = Space(255)
        GetWINPath = ""
        l = GetWindowsDirectory(buff, 255)

        If l > 0 Then GetWINPath = Mid(buff, 1, l)
    End Function
    Public Function MoveToOtherDIR(ByRef sFNam As String, ByRef dFNam As String) As Boolean
        If MoveFile(sFNam & Chr(0), dFNam & Chr(0)) <> 0 Then
            MoveToOtherDIR = True
        Else
            MoveToOtherDIR = False
        End If
    End Function
    Public Function RemoveFile(ByRef fNam As String) As Boolean
        If DeleteFile(fNam & Chr(0)) <> 0 Then
            RemoveFile = True
        Else
            RemoveFile = False
        End If
    End Function
    Public Function GetTPath() As String
        GetTPath = New String(Chr(0), 100)
        If GetTempPath(100, GetTPath) > 0 Then GetTPath = Left(GetTPath, InStr(1, GetTPath, Chr(0), CompareMethod.Binary) - 1)
    End Function
    Public Function GetSearchText(ByRef fNam As String, ByRef sStr As String) As String
        Dim fData As String
        Dim s, e As Integer

        fData = GetFileData(fNam)
        s = 0
        e = 0

        s = InStr(1, fData, sStr, CompareMethod.Text) + Len(sStr)
        If s > Len(sStr) Then
            e = InStr(s, fData, Chr(13), CompareMethod.Binary)
        End If

        If e > s Then
            GetSearchText = Trim(Mid(fData, s, e - s))
        Else
            GetSearchText = ""
        End If
    End Function
    Public Function GetTFileName(ByRef path As String, ByRef prefxStr As String) As String
        Dim tFile As String

        tFile = New String(Chr(0), 260)
        GetTempFileName(path & Chr(0), prefxStr & Chr(0), 0, tFile)
        tFile = Left(tFile, InStr(1, tFile, Chr(0)) - 1)
        SetFileAttributes(tFile, FILE_ATTRIBUTE_TEMPORARY)
        DeleteFile(tFile)

        GetTFileName = Left(tFile, InStr(1, tFile, ".TMP", CompareMethod.Text) - 1)

    End Function
    Public Function DIRExists(ByRef dirName As String) As Boolean
        Dim fHand As Integer
        Dim WFD As WIN32_FIND_DATA = Nothing

        DIRExists = False
        If Len(Trim(dirName)) = 0 Then
            Exit Function
        End If

        fHand = FindFirstFile(dirName, WFD)

        If fHand <> INVALID_HANDLE_VALUE Then
            If Not GetFileAttributes(Trim(dirName) & Chr(0)) And FILE_ATTRIBUTE_DIRECTORY Then
                Exit Function
            End If
        Else
            Exit Function
        End If
        FindClose(fHand)
        DIRExists = True
    End Function
    Public Function FILEExists(ByRef FileName As String) As Boolean
        Dim fHand As Integer
        Dim WFD As WIN32_FIND_DATA = Nothing

        FILEExists = False
        If Len(Trim(FileName)) = 0 Then
            Exit Function
        End If

        fHand = FindFirstFile(FileName, WFD)

        If fHand = INVALID_HANDLE_VALUE Then
            Exit Function
        End If
        FindClose(fHand)
        FILEExists = True
    End Function
    Public Function CreateDIR(ByRef dirName As String) As Boolean
        Dim Security_Renamed As SECURITY_ATTRIBUTES
        Dim ret As Integer

        CreateDIR = False
        ret = CreateDirectory(dirName & Chr(0), Security_Renamed)
        If ret <> 0 Then CreateDIR = True
    End Function
    Public Function GetFileData(ByRef FileName As String) As String
        Dim fHand As Integer
        Dim nSize, ret As Integer
        Dim bBytes() As Byte
        Dim I As Integer
        GetFileData = ""
        fHand = CreateFile(FileName & Chr(0), GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, 0, 0)
        If fHand = INVALID_HANDLE_VALUE Then Exit Function
        nSize = GetFileSize(fHand, 0)
        If nSize <= 0 Then
            CloseHandle(fHand)
            Exit Function
        End If
        ReDim bBytes(nSize)
        ReadFile(fHand, bBytes(1), UBound(bBytes), ret, 0)
        'bBytes (1)
        If ret <> UBound(bBytes) Then Exit Function

        CloseHandle(fHand)

        GetFileData = ""
        For I = 1 To UBound(bBytes)
            GetFileData = GetFileData & Chr(bBytes(I))
        Next

    End Function
    Function GetHeaderData(ByRef mData As String, ByRef sStr As String) As String
        Dim I, j As Integer
        Dim ePtr As Short
        'On Error Resume Next

        GetHeaderData = ""

        I = InStr(1, UCase(mData), UCase(sStr))

        If I <= 0 Then Exit Function

        For j = I To Len(mData)
            If Asc(Mid(mData, j, 1)) < 32 Then
                ePtr = j
                Exit For
            End If
        Next

        If I > 0 Then
            GetHeaderData = Mid(mData, I + Len(sStr), ePtr - (I + Len(sStr)))
        Else
            GetHeaderData = ""
        End If

    End Function
    Public Sub MakeINISection(ByRef Section As String, ByRef sValue As String, ByRef iniName As String)
        Call WritePrivateProfileSection(Section, sValue, iniName)
    End Sub
    Public Function GetINISectionData(ByRef Section As String, ByRef iniName As String) As String
        Dim rtS As String
        Dim rtL As Integer
        GetINISectionData = ""
        rtS = New String(Chr(32), 255)
        rtL = 255
        Call GetPrivateProfileSection(Section, rtS, rtL, iniName)
        If rtL <> 0 Then GetINISectionData = Left(Trim(rtS), Len(Trim(rtS)) - 1)
    End Function
    Public Sub MakeINI(ByRef Section As String, ByRef key As String, ByRef keyValue As String, ByRef iniName As String)
        WritePrivateProfileString(Section & Chr(0), key & Chr(0), keyValue & Chr(0), iniName & Chr(0))
    End Sub
    Public Function GetINIData(ByRef Section As String, ByRef key As String, ByRef iniName As String) As String
        Dim rtS As String
        Dim rtL As Integer

        GetINIData = ""
        rtS = New String(Chr(0), 255)
        rtL = GetPrivateProfileString(Section, key, "Default", rtS, 255, iniName)
        If rtL <> 0 Then GetINIData = Left(rtS, rtL)

    End Function
    Public Function GetFileSaveAs(ByRef hWin As Integer, ByRef aPath As String, Optional ByRef dExt As String = "", Optional ByRef filter_Renamed As String = "", Optional ByRef wTitle As String = "") As String
        Dim fPtr As OPENFILENAME = Nothing


        With fPtr
            .hwndOwner = hWin
            .lpstrDefExt = dExt
            .lpstrFilter = filter_Renamed
            .lpstrTitle = wTitle
            .lpstrInitialDir = aPath
            .nMaxFile = 255
            .lpstrFile = Space(254)
            .lStructSize = Len(fPtr)
        End With

        If GetSaveFileName(fPtr) Then
            GetFileSaveAs = Trim(fPtr.lpstrFile)
        Else
            GetFileSaveAs = ""
        End If

    End Function

    Public Function GetOpenFile(ByRef hWin As Integer, ByRef aPath As String, Optional ByRef dExt As String = "", Optional ByRef filter_Renamed As String = "", Optional ByRef wTitle As String = "") As String
        Dim fPtr As OPENFILENAME = Nothing

        With fPtr
            .hwndOwner = hWin
            .lpstrDefExt = dExt
            .lpstrFilter = filter_Renamed
            .lpstrTitle = wTitle
            .lpstrInitialDir = aPath
            .nMaxFile = 255
            .lpstrFile = Space(254)
            .lStructSize = Len(fPtr)
        End With

        If GetOpenFileName(fPtr) Then
            GetOpenFile = Trim(fPtr.lpstrFile)
        Else
            GetOpenFile = ""
        End If

    End Function

    Function RegQueryStringValue(ByVal hKey As Integer, ByVal strValueName As String) As String
        Dim lValueType, lResult, lDataBufSize As Integer
        Dim strBuf As String
        Dim dataLong As Integer
        RegQueryStringValue = ""
        lResult = RegQueryValueEx(hKey, strValueName, 0, lValueType, 0, lDataBufSize)
        If lResult = 0 Then
            If lValueType = REG_SZ Then
                strBuf = New String(Chr(0), lDataBufSize)
                lResult = RegQueryValueEx(hKey, strValueName, 0, 0, strBuf, lDataBufSize)
                If lResult = 0 Then
                    RegQueryStringValue = Left(strBuf, InStr(1, strBuf, Chr(0)) - 1)
                End If
            ElseIf lValueType = REG_DWORD Then
                lResult = RegQueryValueEx(hKey, strValueName, 0, 0, dataLong, lDataBufSize)
                If lResult = 0 Then
                    RegQueryStringValue = Str(dataLong)
                End If
            End If
        End If
    End Function
    Function GetRegString(ByRef hKey As Integer, ByRef strPath As String, ByRef strValue As String) As Object
        Dim ret As Object = Nothing

        RegOpenKey(hKey, strPath, ret)
        GetRegString = RegQueryStringValue(ret, strValue)
        RegCloseKey(ret)
    End Function
    Sub SaveRegString(ByRef hKey As Integer, ByRef strPath As String, ByRef strValue As String, ByRef strData As String)
        Dim ret As Object = Nothing

        RegCreateKey(hKey, strPath, ret)
        RegSetValueEx(ret, strValue, 0, REG_SZ, strData, Len(strData))
        RegCloseKey(ret)
    End Sub
    Sub SaveRegLong(ByRef hKey As Integer, ByRef strPath As String, ByRef strValue As String, ByRef strData As String)
        Dim ret As Object = Nothing

        RegCreateKey(hKey, strPath, ret)
        RegSetValueEx(ret, strValue, 0, REG_DWORD, CInt(strData), 4)
        RegCloseKey(ret)
    End Sub

    Public Function HTMLString(ByRef txtRTF As String) As String
        Dim strTRF As String
        Dim tStr As String
        Dim tArr() As String
        Dim lTag As Short
        Dim closePara As String
        Dim I As Integer

        MakeFontTable(txtRTF)
        MakeColorTable(txtRTF)

        strTRF = Mid(txtRTF, InStr(txtRTF, start) + Len(start))
        strTRF = Mid(strTRF, 1, Len(strTRF) - (Len(finish) + 2))
        strTRF = Replace(strTRF, para, "<br>")
        closePara = ""

        tArr = Split(strTRF, boundary)
        tStr = ""
        lTag = 0

        For I = 0 To UBound(tArr)
            lTag = 0
            If InStr(tArr(I), "\pard") > 0 Then
                tArr(I) = Replace(tArr(I), "\pard", closePara)
                lTag = 1
            End If
            If Right(tArr(I), 1) = " " Then
                tArr(I) = Trim(tArr(I)) & "&nbsp;"
            End If
            If InStr(tArr(I), "\rquote") > 0 Then
                tArr(I) = Replace(tArr(I), "\rquote", "&rsquo;")
            End If
            If InStr(tArr(I), "\qc") > 0 Then
                tArr(I) = Replace(tArr(I), "\qc", "<Center>")
                closePara = "</center>"
                lTag = 1
            End If
            If InStr(tArr(I), "\qr") > 0 Then
                tArr(I) = Replace(tArr(I), "\qr", "<Right>")
                closePara = "</Right>"
                lTag = 1
            End If
            If InStr(tArr(I), "\strike") > 0 Then
                tArr(I) = Replace(tArr(I), "\strike", "<strike>") & "</strike>"
                lTag = 1
            End If
            If InStr(tArr(I), "\tab") > 0 Then
                tArr(I) = Replace(tArr(I), "\tab", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
                lTag = 1
            End If
            If InStr(tArr(I), "\b") > 0 Then
                tArr(I) = Replace(tArr(I), "\b", "<b>") & "</b>"
                lTag = 1
            End If
            If InStr(tArr(I), "\i") > 0 Then
                tArr(I) = Replace(tArr(I), "\i", "<i>") & "</i>"
                lTag = 1
            End If
            If InStr(tArr(I), "\ul") > 0 Then
                tArr(I) = Replace(tArr(I), "\ul", "<u>") & "</u>"
                lTag = 1
            End If
            If InStr(tArr(I), "\f") > 0 Then
                tArr(I) = RedyFontTag(tArr(I)) & "</font>"
            End If

            tStr = tStr & tArr(I)
        Next

        HTMLString = tStr
    End Function

    Public Function RTFString(ByRef txtRTF As String) As String
        Dim strRTF As String
        Dim tStr As String
        Dim tArr() As String
        Dim lTag As Short
        Dim I As Integer

        strRTF = ""

        For I = 1 To Len(txtRTF)
            If Asc(Mid(txtRTF, I, 1)) >= 32 Then
                strRTF = strRTF & Mid(txtRTF, I, 1)
            End If
        Next

        tArr = Split(strRTF, boundary)
        tStr = ""
        lTag = 0

        For I = 0 To UBound(tArr)
            lTag = 0
            If InStr(tArr(I), "\strike") > 0 Then
                tArr(I) = Replace(tArr(I), "\strike", "{\strike") & "}"
                lTag = 1
            End If
            If InStr(tArr(I), "\tab") > 0 Then
                tArr(I) = Replace(tArr(I), "\tab", "{\tab") & "}"
                lTag = 1
            End If
            If InStr(tArr(I), "\b") > 0 Then
                tArr(I) = Replace(tArr(I), "\b", "{\b") & "}"
                lTag = 1
            End If
            If InStr(tArr(I), "\i") > 0 Then
                tArr(I) = Replace(tArr(I), "\i", "{\i") & "}"
                lTag = 1
            End If
            If InStr(tArr(I), "\ul") > 0 Then
                tArr(I) = Replace(tArr(I), "\ul", "{\ul") & "}"
                lTag = 1
            End If
            If InStr(tArr(I), "\f") > 0 Then 'And lTag = 0
                tArr(I) = RedyFontRTF(tArr(I))
            End If
            tStr = tStr & tArr(I)
        Next

        RTFString = Replace(tStr, "\plain", "")
    End Function


    'Public Sub RedyMailQList()
    '    Dim fHand As Long, LErr As Long
    '    Dim WFD As WIN32_FIND_DATA
    '    Dim fNam As String
    '    Dim i As Long
    '
    '    fHand = FindFirstFile(App.path & "\Mail\Queue\*.eml", WFD)
    '    i = 0
    '    If fHand <> INVALID_HANDLE_VALUE Then
    '        Do While True
    '            i = i + 1
    '            fNam = Left$(WFD.cFileName, InStr(WFD.cFileName, Chr$(0)) - 1)
    '            frmSendMail.listVQ.ListItems.Add , "MQ#" & fNam, Replace(Replace(GetSearchText( _
    ''                App.path & "\Mail\Queue\" & fNam, "To: "), "<", ""), ">", "")
    '            If FindNextFile(fHand, WFD) = 0 Then
    '                Exit Do
    '            End If
    '        Loop
    '    End If
    '    FindClose fHand
    '
    'End Sub

    Public Sub RedyMailList(ByRef obj As Object)
        'Dim fHand, LErr As Integer
        'Dim WFD As WIN32_FIND_DATA
        'Dim fNam As String
        'Dim I As Integer

        'obj.Nodes.Add(, , "M", "Mail")
        'obj.Nodes.Add("M", ComctlLib.TreeRelationshipConstants.tvwChild, "MQ", "Queue")
        'obj.Nodes.Add("M", ComctlLib.TreeRelationshipConstants.tvwChild, "MS", "Send")

        'fHand = FindFirstFile(My.Application.Info.DirectoryPath & "\Mail\Queue\*.eml", WFD)
        'I = 0
        'If fHand <> INVALID_HANDLE_VALUE Then
        '    Do While True
        '        I = I + 1
        '        fNam = Left(WFD.cFileName, InStr(WFD.cFileName, Chr(0)) - 1)
        '        obj.Nodes.Add("MQ", ComctlLib.TreeRelationshipConstants.tvwChild, "MQ#" & fNam, Replace(Replace(GetSearchText(My.Application.Info.DirectoryPath & "\Mail\Queue\" & fNam, "To: "), "<", ""), ">", ""))
        '        If FindNextFile(fHand, WFD) = 0 Then
        '            Exit Do
        '        End If
        '    Loop
        'End If
        'FindClose(fHand)
        'obj.Nodes(2).Text = obj.Nodes(2).Text & " [ " & I & " ]"

        'fHand = FindFirstFile(My.Application.Info.DirectoryPath & "\Mail\Send\*.eml", WFD)
        'I = 0
        'If fHand <> INVALID_HANDLE_VALUE Then
        '    Do While True
        '        fNam = Left(WFD.cFileName, InStr(WFD.cFileName, Chr(0)) - 1)
        '        I = I + 1
        '        obj.Nodes.Add("MS", ComctlLib.TreeRelationshipConstants.tvwChild, "MS#" & fNam, Replace(Replace(GetSearchText(My.Application.Info.DirectoryPath & "\Mail\Send\" & fNam, "To: "), "<", ""), ">", ""))
        '        If FindNextFile(fHand, WFD) = 0 Then
        '            Exit Do
        '        End If
        '    Loop
        'End If
        'FindClose(fHand)
        'obj.Nodes(3).Text = obj.Nodes(3).Text & " [ " & I & " ]"

    End Sub

    Public Sub MakeFontTable(ByRef txtRTF As String)
        Dim ePtr, sPtr, l As Integer
        Dim I As Integer
        Dim tStr As String

        sPtr = InStr(1, txtRTF, "{\fonttbl") + 9
        ePtr = InStr(sPtr, txtRTF, "}}") + 1
        l = ePtr - sPtr
        tStr = Mid(txtRTF, sPtr, l)
        tStr = Replace(tStr, "{", "")
        tStr = Replace(tStr, "}", "")

        fonttbl = Split(tStr, ";")

        For I = 0 To UBound(fonttbl) - 1 Step 1
            fonttbl(I) = Trim(Mid(fonttbl(I), InStr(1, fonttbl(I), " ")))
        Next

    End Sub
    Public Sub MakeColorTable(ByRef txtRTF As String)
        Dim ePtr, sPtr, l As Integer
        Dim I As Integer
        Dim tStr As String
        Dim tArr() As String

        sPtr = InStr(1, txtRTF, "{\colortbl\") + 11
        ePtr = InStr(sPtr, txtRTF, ";}") + 1
        l = ePtr - sPtr
        tStr = Mid(txtRTF, sPtr, l)
        tStr = Replace(tStr, "{", "")
        tStr = Replace(tStr, "}", "")

        colortbl = Split(tStr, ";")

        For I = 0 To UBound(colortbl) - 1 Step 1
            tArr = Split(Trim(Replace(colortbl(I), "\", " ")), " ")
            colortbl(I) = Str(RGB(CInt(Mid(tArr(0), 4)), CInt(Mid(tArr(1), 6)), CInt(Mid(tArr(2), 5))))
        Next

    End Sub
    Public Function RedyFontTag(ByRef fStr As String) As String
        Dim fS As String
        Dim fN As String
        Dim fC As String
        Dim l, s, I As Integer
        Dim tStr As String


        On Error GoTo endFunc

        fC = ""
        s = InStr(1, fStr, "\cf") + 3
        If s > 3 Then
            l = 0
            For I = s To Len(fStr)
                If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                    l = l + 1
                Else
                    Exit For
                End If
            Next

            fC = colortbl(CInt(Mid(fStr, s, l)))
            fStr = Replace(fStr, "\cf" & Mid(fStr, s, l), "")
        End If

        s = InStr(1, fStr, "\fs") + 3
        l = 0
        For I = s To Len(fStr)
            If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                l = l + 1
            Else
                Exit For
            End If
        Next

        fS = Str(Int(Int(CDbl(Mid(fStr, s, l))) / 6))
        fStr = Replace(fStr, "\fs" & Mid(fStr, s, l), "")

        s = InStr(1, fStr, "\f") + 2
        l = 0
        For I = s To Len(fStr)
            If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                l = l + 1
            Else
                Exit For
            End If
        Next
        fN = fonttbl(CInt(Mid(fStr, s, l)))
        fStr = Replace(fStr, "\f" & Mid(fStr, s, l), "")

        If fC <> "" Then
            tStr = "<Font face='" & Trim(fN) & "' size='" & Trim(fS) & "' color='#" & Hex(CInt(Trim(fC))) & "' >"
        Else
            tStr = "<Font face='" & Trim(fN) & "' size='" & Trim(fS) & "' >"
        End If

        If Trim(fStr) = "" Then
endFunc:
            RedyFontTag = " "
            Exit Function
        End If

        tStr = tStr & fStr
        RedyFontTag = tStr

    End Function
    Public Function RedyFontRTF(ByRef fStr As String) As String
        Dim fS As String
        Dim fN As String = ""
        Dim fC As String
        Dim I, s, l As Integer
        Dim tStr As String

        On Error GoTo endFunc

        fC = ""
        s = InStr(1, fStr, "\cf") + 3
        If s > 3 Then
            l = 0
            For I = s To Len(fStr)
                If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                    l = l + 1
                Else
                    Exit For
                End If
            Next

            fStr = Replace(fStr, "\cf" & Mid(fStr, s, l), "")
        End If

        s = InStr(1, fStr, "\fs") + 3
        l = 0
        For I = s To Len(fStr)
            If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                l = l + 1
            Else
                Exit For
            End If
        Next

        fS = Str(Int(Int(CDbl(Mid(fStr, s, l)))))
        fStr = Replace(fStr, "\fs" & Mid(fStr, s, l), "")

        s = InStr(1, fStr, "\f") + 2
        l = 0
        For I = s To Len(fStr)
            If IsNumeric(Trim(Mid(fStr, I, 1))) Then
                l = l + 1
            Else
                Exit For
            End If
        Next

        fStr = Replace(fStr, "\f" & Mid(fStr, s, l), "")

        If fC <> "" Then
            tStr = "<Font face='" & Trim(fN) & "' size='" & Trim(fS) & "' color='#" & Hex(CInt(Trim(fC))) & "' >"
        End If

        If Trim(fStr) = "" Then
endFunc:
            RedyFontRTF = " "
            Exit Function
        End If

        tStr = "{\fs" & Trim(fS) & fStr & "}"
        RedyFontRTF = tStr

    End Function

    'Public Function WriteInI(Section As String, ByVal KeyName As String, ByVal DefaultValue As String, BarFileName As String)
    'On Error GoTo WriteErr
    'Dim FileName
    '    FileName = App.path & "\" & BarFileName
    '    WritePrivateProfileString Section, KeyName, DefaultValue, FileName
    'Exit Function
    'WriteErr:
    'MsgBox "[" & err.Number & "] " & err.Description, vbInformation, "WriteInI - Error"
    'End Function


    'Public Function ReadInI(Section As String, ByVal KeyName As String, BarFileName As String) As String
    'On Error GoTo ReadIniErr
    'Dim Default, FileName, ReturnString$, ReturnStr
    'Dim Valid%
    '    FileName = App.path & "\" & BarFileName
    '    Default = ""
    '    ReturnString$ = Space(100)
    '    Valid% = GetPrivateProfileString(Section, KeyName, Default, ReturnString, Len(ReturnString) + 1, FileName)
    '    ReturnStr = Left$(ReturnString$, Valid%)
    '    ReadInI = ReturnStr
    'Exit Function
    'ReadIniErr:
    'MsgBox "[" & err.Number & "] " & err.Description, vbInformation, "ReadInI - Error"
    'End Function

    Public Function GetErrorMSG(ByRef mVal As Short) As String
        GetErrorMSG = ""
        Select Case mVal
            Case 1
                GetErrorMSG = "An unknown error has occurred"
            Case 2
                GetErrorMSG = "An error has resulted because there was no message specified"
            Case 3
                GetErrorMSG = "The process has run out of memory."
            Case 4
                GetErrorMSG = "An error has occurred due to a problem with the message body or attachments."
            Case 5
                GetErrorMSG = "There was a problem initiating the conversation with the mail server. Ensure the setting of the Domain property is correct."
            Case 6
                GetErrorMSG = "There was an error terminating the conversation with the SMTP mail server."
            Case 7
                GetErrorMSG = "The from address was not formatted correctly or was rejected by the SMTP mail server. Some SMTP mail servers will only accept mail from particular addresses or domains. SMTP mail servers may also reject a from address if the server can not successfully do a reverse lookup on the from address."
            Case 8
                GetErrorMSG = "An error was reported in response to a recipient address. The SMTP server may refuse to handle mail for unknown recipients."
            Case 9
                GetErrorMSG = "There was an error connecting to the SMTP mail server."
            Case 10
                GetErrorMSG = "There was an error opening a file. If you have specified file attachments, ensure that they exist and the you have access to them."
            Case 11
                GetErrorMSG = "There was an error reading a file. If you have specified file attachments, ensure that they exist and the you have access to them."
            Case 12
                GetErrorMSG = "There was an error writing to a file. Ensure that you have sufficient disk space."
            Case 15
                GetErrorMSG = "No mail server specified."
            Case 16
                GetErrorMSG = "There was a problem with the connection and a socket error occured."
            Case 17
                GetErrorMSG = "Could not resolve host."
            Case 18
                GetErrorMSG = "Connected but server sent back bad response."
            Case 19
                GetErrorMSG = "Could not create thread."
            Case 20
                GetErrorMSG = "Cancelled as a result of calling the Cancel() method."
            Case 21
                GetErrorMSG = "The operation timed-out while the host was being resolved."
            Case 22
                GetErrorMSG = "The operation timed-out while connecting."
            Case 24
                GetErrorMSG = "ESMTP Authentication failed."
            Case 25
                GetErrorMSG = "The selected ESMTP Authentication mode is not supported by the server."
            Case 26
                GetErrorMSG = "ESMTP Authentication protocol error."
            Case 27
                GetErrorMSG = "Socket Timeout Error"
        End Select
    End Function
End Module
